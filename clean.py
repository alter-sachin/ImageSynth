import os
import shutil


x = os.listdir("train")
os.chdir("train")


for images in x:
	if "img" in images:
		#y = os.listdir(str(images))
		shutil.mv(str(images),"../raw")