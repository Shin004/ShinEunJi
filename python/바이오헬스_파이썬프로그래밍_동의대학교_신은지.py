from tkinter import *
from tkinter.filedialog import *
from tkinter.simpledialog import *
from PIL import Image, ImageFilter, ImageEnhance, ImageOps, ImageTk
import os
import random


# 함수 선언 부분
def displayPhoto(img, width, height):
    global root, canvas
    root.geometry(str(width) + "x" + str(height))
    image = ImageTk.PhotoImage(img)

    if canvas is not None:
        canvas.destroy()

    canvas = Canvas(root, width=width, height=height)
    canvas.create_image(0, 0, anchor=NW, image=image)
    canvas.pack()
    root.mainloop()

def update_out_photo():
    global outPhoto, outX, outY
    outX, outY = outPhoto.size
    displayPhoto(outPhoto, outX, outY)

def func_open():
    global inPhoto, inX, inY, outPhoto
    filename = askopenfilename(parent=root, filetypes=(("모든 그림파일", "*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tif"), ("모든 파일", "*.*")))
    inPhoto = Image.open(filename)
    inX, inY = inPhoto.size
    outPhoto = inPhoto.copy()
    update_out_photo()

    user_input()

def user_input():
    degree = askinteger("숫자입력", "0~7까지 입력 해주세요!", minvalue=0, maxvalue=7)
    process_user_input(degree)

def func_save():
    global root, canvas, inPhoto, outPhoto, inX, inY
    if outPhoto is None:
        return
    saveFp = asksaveasfile(parent=root, mode="w",
                           defaultextension=".jpg",
                           filetypes=(("JPG 파일", "*.jpg;*.jpeg"), ("모든 파일", "*.*")))
    if saveFp:
        outPhoto.save(saveFp.name)

def apply_effect(effect):
    global outPhoto
    if outPhoto is None:
        return
    outPhoto = effect(outPhoto)
    update_out_photo()


def func_mirror1():
    def mirror2(img):
        return img.transpose(Image.FLIP_TOP_BOTTOM)
    apply_effect(mirror2)

def func_mirror2():
    def mirror2(img):
        return img.transpose(Image.FLIP_LEFT_RIGHT)
    apply_effect(mirror2)

def func_rotate():
    def rotate(img):
        degree = askinteger("회전", "회전할 각도를 입력하세요", minvalue=0, maxvalue=360)
        return img.rotate(degree, expand=True)
    apply_effect(rotate)


def func_embos():
    def apply_emboss(img):
        return img.filter(ImageFilter.EMBOSS)
    apply_effect(apply_emboss)


def func_sketch():
    def apply_sketch(img):
        return img.filter(ImageFilter.CONTOUR)
    apply_effect(apply_sketch)

def func_contour():
    def apply_contour(img):
        return img.filter(ImageFilter.FIND_EDGES)
    apply_effect(apply_contour)

def func_blackwhite():
    def convert_to_blackwhite(img):
        return img.convert("L")
    apply_effect(convert_to_blackwhite)



def read_random_image():
    image_files = [file for file in os.listdir() if file.endswith((".png", ".jpg", ".jpeg", ".bmp", ".gif", ".tif"))]
    if not image_files:
        print("현재 디렉토리에 이미지 파일이 없습니다.")
        return None

    random_image_file = random.choice(image_files)

    try:
        image = Image.open(random_image_file)
        return image
    except Exception as e:
        print(f"이미지를 열 수 없습니다: {e}")
        return None


def process_user_input(user_input):

    
    if user_input == 1:
        func_mirror2()
    elif user_input == 2:
        func_mirror1()
    elif user_input == 3:
        func_rotate()
    elif user_input == 4:
        func_blackwhite()
    elif user_input == 5:
        func_embos()
    elif user_input == 6:
        func_sketch()
    elif user_input == 7:
        func_contour()
    elif user_input == 0:
        root.quit()

# 전역 변수 선언 부분
root = Tk()
root.geometry("500x500")
root.title("포토 에디터")

canvas = None
inPhoto = None
outPhoto = None
inX = 0
inY = 0
outX = 0
outY = 0


# 메인 코드 부분
mainMenu = Menu(root)
root.config(menu=mainMenu)

fileMenu = Menu(mainMenu)
mainMenu.add_cascade(label="파일", menu=fileMenu)
fileMenu.add_command(label="파일 열기", command=func_open)
fileMenu.add_command(label="파일 저장")
fileMenu.add_separator()
fileMenu.add_command(label="숫자 입력",command=user_input)
##fileMenu.add_command(label="프로그램 종료", command=root.quit)







##effectMenu = Menu(mainMenu)
##mainMenu.add_cascade(label="이미지 처리", menu=effectMenu)
##effectMenu.add_command(label="상하 반전", command=func_mirror2)
##effectMenu.add_command(label="좌우 반전", command=func_mirror1)
##effectMenu.add_command(label="회전", command=func_rotate)
##effectMenu.add_command(label="흑백", command=func_blackwhite)
##effectMenu.add_command(label="엠보싱", command=func_embos)
##effectMenu.add_command(label="연필스케치", command=func_sketch)
##effectMenu.add_command(label="경계선추출", command=func_contour)

root.mainloop()
