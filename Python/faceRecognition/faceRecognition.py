# Import
import cv2 as cv

# Constantes
# Image ou vidéo
IMG_OR_VID = False
# Vidéo ou webcam
VID_OR_CAM = False
# Dessiner le rectangle pour signaler le visage ou pas
DRAW_RECT_FACE = False
# Dossiers
FOLDER_MEDIAS = 'medias/'
FOLDER_HAAR_CASCADE = 'haarCascade/'
# Fichiers
FILENAME_IMG = FOLDER_MEDIAS + 'faces.jpeg'
FILENAME_VID = FOLDER_MEDIAS + 'faces.mp4'
FILENAME_MASK = FOLDER_MEDIAS + 'mask.png'
# Masque
MASK = cv.imread(FILENAME_MASK, cv.IMREAD_UNCHANGED)
# Rectangle pour signaler les visages
RECT_FACE_COLOR = (255, 0, 0)
RECT_FACE_THICKNESS = 2
# Paramètres de la détection de visage
PARAM_FACE_DETECT_SCALE_FACTOR = 1.1
PARAM_FACE_DETECT_MIN_NEIGHBORS = 15
# Paramètres de la détection de bouche
PARAM_MOUTH_DETECT_SCALE_FACTOR = 1.7
PARAM_MOUTH_DETECT_MIN_NEIGHBORS = 11
# Charger les fichiers permettant de reconnaître les visages
FACE_CASCADE = cv.CascadeClassifier(cv.data.haarcascades + "haarcascade_frontalface_default.xml")
MOUTH_CASCADE = cv.CascadeClassifier(FOLDER_HAAR_CASCADE + "haarcascade_mouth.xml")

# Fonctions

# Dessiner des rectangles sur les endroits donnés
def drawRect(detected, img, rectColor = RECT_FACE_COLOR, rectThickness = RECT_FACE_THICKNESS):
	# Parcourir les endroits donnés
	for (x, y, w, h) in detected:
		cv.rectangle(img, (x, y), (w + x, h + y), rectColor, rectThickness)

	return img

# Dessiner des rectangles sur les bouches trouvées
def drawMask(mouths, img):
	# Pouvoir accéder à la variable globale (déclarée en globale pour éviter de devoir charger l'image à chaque fois)
	global MASK

	# Parcourir les bouches
	for (x, y, w, h) in mouths:
		# Dimensionner le masque pour prendre la moitié du visage
		MASK = cv.resize(MASK, (w, h))
		# Copier le masque sur la bouche
		img[y:y+h, x:x+w] = cv.addWeighted(img[y:y+h, x:x+w], 1, MASK, 1, 0)

	return img

# Cette fonction détecte les visages sur l'image donnée en argument
def detectFace(grayImg):
	# Détection des visages
	return FACE_CASCADE.detectMultiScale(grayImg, PARAM_FACE_DETECT_SCALE_FACTOR, PARAM_FACE_DETECT_MIN_NEIGHBORS)

# Cette fonction détecte la bouche sur l'image donnée en argument
def detectMouth(faces, grayImg):
	mouths = []
	# Parcourir les visages
	for (x, y, w, h) in faces:
		# Récupérer seulement le visage
		cropped = grayImg[y:y+h, x:x+w] # Indexation numpy : (y, x)
		# Détection de la bouche
		mouth = MOUTH_CASCADE.detectMultiScale(cropped, PARAM_MOUTH_DETECT_SCALE_FACTOR, PARAM_MOUTH_DETECT_MIN_NEIGHBORS)

		# Une bouche a été trouvée
		if type(mouth) is not tuple:
			midHeight = int(h/2)
			# Prendre en compte la position de la bouche dans l'image d'origine
			mouth[0][0] = x
			mouth[0][1] = y + midHeight
			mouth[0][2] = w
			mouth[0][3] = midHeight
			# Ajouter une seule bouche
			mouths.append(mouth[0])

	return mouths

# Sur l'image donnée, trouve les visages, les bouches et ajoute un masque
def detecFaceAndAddMask(img):
	# Transformer l'image en RGBA
	img = cv.cvtColor(img, cv.COLOR_RGB2RGBA)
	# Convertir l'image en niveau de gris
	gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)

	# Détecter les visages sur l'image
	faces = detectFace(gray)
	# Détecter les bouches sur l'image
	mouths = detectMouth(faces, gray)

	# Dessiner un rectangle sur chaque visage
	if DRAW_RECT_FACE:
		img = drawRect(faces, img)
	# Dessiner un masque sur chaque bouche
	img = drawMask(mouths, img)

	# Affichage
	cv.imshow('Mask', img)

# Pour une image
def img():
	# Charger l'image
	img = cv.imread(FILENAME_IMG, cv.IMREAD_UNCHANGED)

	# Traitement
	detecFaceAndAddMask(img)

# Pour une vidéo
def video():
	# Vidéo ou webcam
	if VID_OR_CAM:
		# Charger la vidéo
		vid = cv.VideoCapture(FILENAME_VID)
	else:
		# Récupérer le flux de la webcam
		vid = cv.VideoCapture(0)

	vidUnfinished = True

	# Tant que la vidéo n'est pas finie
	while vidUnfinished:
		# Lire la frame
		vidUnfinished, img = vid.read()

		# Traitement
		detecFaceAndAddMask(img)

		# WaitKey sinon la fenêtre ne s'affiche pas
		cv.waitKey(1)

	# Libérer la vidéo
	vid.release()

# Fonction principale
def main():
	# Image ou vidéo
	if IMG_OR_VID:
		img()
	else:
		video()

	# Fermer la fenêtre lorsqu'on appuie sur une touche
	cv.waitKey(0)
	cv.destroyAllWindows()

# Lancer la fonction main de base
if __name__ == '__main__':
	main()