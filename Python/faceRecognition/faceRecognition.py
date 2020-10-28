# Import
import numpy as np
import cv2

# Fonction principale
def main():
	# Charger l'image
	img = cv2.imread('wallhaven-vg7lv3.jpg', 1)
	# Affichage
	cv2.imshow('image', img)
	# Fermer la fenêtre lorsqu'on appuie sur une touche
	cv2.waitKey(0)
	cv2.destroyAllWindows()

if __name__ == '__main__':
	main()