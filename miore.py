from tkinter import *
import time

gui = Tk()
w = 1280
h = 700
c = Canvas(gui ,width=w ,height=h)
c.create_rectangle(0,0,w,h,fill='#ff3276')
c.pack(expand=True)
# Creates all vertical lines at intevals of 3
lim = max(h,w)
vert = []
for i in range(0, lim, 3):
    line = c.create_line([(i, 0), (i, h)], tag='grid_line', width = 1 )
    vert.append(line)
# Creates all horizontal lines at intevals of 3
horiz = []
for i in range(0, lim, 3): 

    line = c.create_line([(0, i), (w, i)], tag='grid_line', width = 2)
    horiz.append(line)

#creates background ovals
for i in range(0,lim,4):
    oval = c.create_oval((w/2)+(i*2),(h/2)+i,(w/2)-(i*2),(h/2)-i, width = 1)

# Creates ovals and movement vector initialized to (3,3)
ovals = []
span = max(h,w)//5
for i in range(0,span,5):
    oval = c.create_oval((w/2)+i,(h/2)+i,(w/2)-i,(h/2)-i)
    ovals.append([oval,2,0])
    #gui.update()
    #time.sleep(.0005)




# Move Ovals
"""while True:
    i = len(ovals) - 1
    while i>=0:
        oval = ovals[i]
        xd = oval[1]
        yd = oval[2]
        if i == len(ovals)-1:
            bdl = 0
            bdr = w
        else:
            p = c.coords(ovals[i+1][0])
            bdl = p[0]
            bdr = p[2] 
        p = c.coords(oval[0])
        if (p[0]+xd>bdl) and (p[2]+xd<bdr):    
            c.move(oval[0],xd,yd)
        else:
            oval[1] = -xd
            c.move(oval[0],xd,yd)
        i = i - 1
    gui.update()
    time.sleep(0.005)"""

ubd = h
lbd = 0


#bouncing ball
xd = -7
yd = 11
while True:
    ys = 2
    xs = 2
    outside = ovals[len(ovals)-1]
    p = c.coords(outside[0])
    if p[0]<=0 or p[2]>=w:
        xd = -xd
    if p[1]<= lbd or p[3]>=ubd:
        yd = -yd
    for oval in ovals:
        c.move(oval[0],xd,yd)
    gui.update()
    time.sleep(0.0002)

"""
#fill screen with movement
xd = -6
yd = 5
while True:
    outside = ovals[len(ovals)//5-1]
    p = c.coords(outside[0])
    if p[0]<=0 or p[2]>=w:
        xd = -xd
    if p[1]<= lbd or p[3]>=ubd:
        yd = -yd
    for oval in ovals:
        c.move(oval[0],xd,yd)
    gui.update()
    time.sleep(0.005)
"""


gui.mainloop()