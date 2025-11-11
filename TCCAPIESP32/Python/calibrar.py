# -*- coding: utf-8 -*-

import cv2
import numpy as np
import json
import os


def salvar_calibracao(fator_escala, y_base, linha_inicial_pts):
    dados = {
        "fator_escala": fator_escala,
        "y_base": y_base,
        "linha_inicial_pts": linha_inicial_pts
    }
    with open("calibracao.json", "w") as f:
        json.dump(dados, f, indent=4)
    print("💾 Calibração salva em calibracao.json!")


def calibrar(pasta_imagens="imagens/", altura_real_cm=10):
    pontos = []
    calibrado = False
    fator_escala = 1.0
    linha_inicial_pts = None
    y_base = None

    def mouse_callback(event, x, y, flags, param):
        nonlocal pontos, calibrado, fator_escala, linha_inicial_pts, y_base
        if event == cv2.EVENT_LBUTTONDOWN:
            pontos.append((x, y))
            if len(pontos) == 2:
                linha_inicial_pts = (pontos[0], pontos[1])
                print(f"🟢 Linha da água definida: {linha_inicial_pts}")
            elif len(pontos) == 4 and not calibrado:
                distancia_px = abs(pontos[2][1] - pontos[3][1])
                fator_escala = altura_real_cm / distancia_px
                y_base = pontos[2][1]
                calibrado = True
                print(f"✅ Calibrado! 1 pixel = {fator_escala:.3f} cm")
                salvar_calibracao(fator_escala, y_base, linha_inicial_pts)

    imagem_inicial = cv2.imread(os.path.join(pasta_imagens, "calibrar.jpg"))
    if imagem_inicial is None:
        print("❌ Erro: imagem 'calibrar.jpg' não encontrada na pasta:", pasta_imagens)
        return

    cv2.namedWindow("Calibracao")
    cv2.setMouseCallback("Calibracao", mouse_callback)

    print("1️⃣ Clique 2 pontos nas margens (linha da água).")
    print("2️⃣ Clique 2 pontos na régua (0 cm e 600 cm).")
    print("🟢 Pressione 'q' quando terminar a calibração.")

    while True:
        img_copy = imagem_inicial.copy()
        for p in pontos:
            cv2.circle(img_copy, p, 5, (0, 0, 255), -1)
        if len(pontos) >= 2:
            cv2.line(img_copy, pontos[0], pontos[1], (0, 0, 255), 2)
        cv2.imshow("Calibracao", img_copy)
        if cv2.waitKey(1) & 0xFF == ord("q"):
            break

    cv2.destroyAllWindows()

    if calibrado:
        print("✅ Calibração concluída e salva!")
    else:
        print("⚠️ Calibração não concluída. Nenhum arquivo salvo.")


if __name__ == "__main__":
    calibrar()
