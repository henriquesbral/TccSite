# -*- coding: utf-8 -*-

import sys
import cv2
import numpy as np
import json
import os

def carregar_calibracao():
    caminho = "calibracao.json"
    if not os.path.exists(caminho):
        print("❌ Erro: arquivo de calibração não encontrado.")
        sys.exit(1)
    with open(caminho, "r") as f:
        return json.load(f)

def detectar_linha_agua(img, calibracao):
    (x1, y1), (x2, y2) = map(tuple, calibracao["linha_inicial_pts"])
    y_base = calibracao["y_base"]
    fator_escala = calibracao["fator_escala"]

    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    blur = cv2.GaussianBlur(gray, (7, 7), 0)
    edges = cv2.Canny(blur, 50, 150)

    altura_roi = 40
    mask = np.zeros_like(edges)
    pts_roi = np.array(
        [
            [x1, y1 - altura_roi // 2],
            [x2, y2 - altura_roi // 2],
            [x2, y2 + altura_roi // 2],
            [x1, y1 + altura_roi // 2],
        ],
        dtype=np.int32,
    )
    cv2.fillPoly(mask, [pts_roi], 255)
    roi = cv2.bitwise_and(edges, mask)

    soma_linhas = np.sum(roi, axis=1)
    linha_detectada_y = np.argmax(soma_linhas)
    altura_cm = abs(y_base - linha_detectada_y) * fator_escala

    return altura_cm

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Uso: python processar.py <caminho_imagem>")
        sys.exit(1)

    image_path = sys.argv[1]
    img = cv2.imread(image_path)
    if img is None:
        print("❌ Erro: imagem não encontrada em", image_path)
        sys.exit(1)

    calibracao = carregar_calibracao()
    nivel = detectar_linha_agua(img, calibracao)
    print(f"{nivel:.2f}")
