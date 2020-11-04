# Import
import cv2 as cv

# Constantes
# Image ou vidéo
IMG_OR_VID = True
# Fichiers
FILENAME_IMG = 'faces.jpeg'
FILENAME_VID = 'faces.mp4'
FILENAME_MASK = 'mask.png'
# Rectangle pour signaler les visages
RECTANGLE_FACE_COLOR = (255, 0, 0)
RECTANGLE_FACE_THICKNESS = 2
# Paramètres de la détection de visage
PARAM_FACE_DETECT_SCALE_FACTOR = 1.1
PARAM_FACE_DETECT_MIN_NEIGHBORS = 15
# Charger les fichiers permettant de reconnaître les visages
FACE_CASCADE = cv.CascadeClassifier(cv.data.haarcascades + "haarcascade_frontalface_default.xml")
MOUTH_CASCADE = cv.CascadeClassifier("haarcascade_mouth.xml")

# Fonctions

# Dessiner des rectangles sur les visages trouvé
def drawRect(detected, img):
	# Parcourir les visages
	for (x, y, w, h) in detected:
		cv.rectangle(img, (x, y), (w + x, h + y), RECTANGLE_FACE_COLOR, RECTANGLE_FACE_THICKNESS)

	return img

# Cette fonction détecte les visages sur l'image donnée en argument
def detectFace(img):
	# Convertir l'image en niveau de gris
	gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
	# Détection des visages
	faces = FACE_CASCADE.detectMultiScale(gray, PARAM_FACE_DETECT_SCALE_FACTOR, PARAM_FACE_DETECT_MIN_NEIGHBORS)

	return faces

# Pour une image
def img():
	# Charger l'image
	img = cv.imread(FILENAME_IMG, cv.IMREAD_UNCHANGED)

	# Détecter les visages sur l'image et dessiner des rectangles dessus
	img = drawRect(detectFace(img), img)

	# Affichage
	cv.imshow('image', img)

# Pour une vidéo
def video():
	# Charger la vidéo
	vid = cv.VideoCapture(FILENAME_VID)

	vidUnfinished = True

	while vidUnfinished:
		# Lire la frame
		vidUnfinished, img = vid.read()

		# Détecter les visages sur l'image et dessiner des rectangles dessus
		img = drawRect(detectFace(img), img)

		# Affichage
		cv.imshow('image', img)

		cv.waitKey(1)

	vid.release()


# Fonction principale
def main():
	print(cv.data.haarcascades)
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