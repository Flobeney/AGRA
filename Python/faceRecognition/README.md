# Projet AGRA

[Lien du GitHub](https://github.com/Flobeney/AGRA/tree/master/Python/faceRecognition)

## Instructions de lancement

* Il faut installer la librairie [opencv-python](https://pypi.org/project/opencv-python/) avec la commande `pip install opencv-contrib-python`
* Il y a plusieurs paramètres qui peuvent être réglés directement dans le programme, en modifiant certaines constantes :
  * `IMG_OR_VID` : si on va chercher un image ou une vidéo
  * `VID_OR_CAM` : si on va chercher une vidéo déjà existante ou si on récupère le flux de la webcam
  * `DRAW_RECT_FACE` : si on signale les visages avec un rectangle
* Par défaut, le programme récupère le flux de la webcam et ne signale pas les visages

## Description du projet

Ce programme a un seul but très simple : ajouter un masque sur les bouches qu'il trouve, ce qui est assez tendance actuellement 
