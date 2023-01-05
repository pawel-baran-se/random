from tkinter import *
from MomentOfInertia import MomentOfInteria
from tkinter import messagebox
from PIL import Image, ImageTk


class MomentOfInertiaGui(object):

    def __init__(self, root):

        self.youngm1 = IntVar()
        self.youngm2 = IntVar()
        '''self.area1 = IntVar()
        self.area1 = IntVar()
        self.inertia1 = IntVar()
        self.inertia2 = IntVar()
        self.dim1 = IntVar()
        self.dim2 = IntVar()
        self.dim3 = IntVar()'''

        self.frame = Frame(root, borderwidth=5)

        data_label = LabelFrame(self.frame, text = 'Data:')
        data_label.grid(sticky=N, row=0, column=0, columnspan=5, padx=5, pady=5)

        # Image file
        image_label = LabelFrame(self.frame, text = 'View:')
        image_label.grid(sticky=W, row=0, column=6, columnspan=5, padx=5, pady=5)
        try:
            load = Image.open("Moment_of_inertia.jpg")
            load_resized = load.resize((320,385))
            render = ImageTk.PhotoImage(load_resized)
            photo_label = Label(image_label, image = render)
            photo_label.image = render
            photo_label.grid(sticky=W, row=1, column=0, padx=5, pady=5)
        except FileNotFoundError:
            photo_label = Label(image_label, text = 'Image not found!')
            photo_label.grid(sticky=W, row=1, column=0, padx=5, pady=5)

        # Youngs modulus 

        Youngs_frame = Frame(data_label)
        Youngs_frame.grid(sticky = W , row = 0, column = 0, columnspan=5)

        Youngs_label = Label(Youngs_frame, text = 'Please choose type of material for each part:')
        Youngs_label.grid(sticky=W, row=0, column=0, columnspan=5, padx=5, pady=5)
        
        Youngs1_label = Label(Youngs_frame, text = 'Part 1:')
        Youngs1_label.grid(sticky=W, row=2, column=0, padx=5, pady=5)

        Y1 = Radiobutton(Youngs_frame, text ='Aluminum 70 GPa', variable = self.youngm1, value = 70)
        Y1.grid(row=2, column=1, padx=5, pady=5)
        Y1.select()

        Y2 = Radiobutton(Youngs_frame, text ='Steel 210 GPa', variable = self.youngm1, value = 210)
        Y2.grid(row=2, column=2, padx=5, pady=5)
        
        Youngs2_label = Label(Youngs_frame, text = 'Part 2:')
        Youngs2_label.grid(sticky=W, row=4, column=0, padx=5, pady=5)

        Y3 = Radiobutton(Youngs_frame, text ='Aluminum 70 GPa', variable = self.youngm2, value = 70)
        Y3.grid(row=4, column=1, padx=5, pady=5)

        Y4 = Radiobutton(Youngs_frame, text ='Steel 210 GPa', variable = self.youngm2, value = 210)
        Y4.grid(row=4, column=2, padx=5, pady=5)
        Y4.select()

        weight_label = Label(Youngs_frame, text= 'Weighted Value:')
        weight_label.grid(row = 5, column = 0, padx=5, pady=5, stick = 'W')

        # Area & Moment of inertia & Distances

        Entry_frame = Frame(data_label)
        Entry_frame.grid(sticky = W , row = 1, column = 0, columnspan=5)

        Area_label = Label(Entry_frame, text = 'Area of each part:')
        Area_label.grid(sticky=W, row=0, column=0, columnspan=5, padx=5, pady=5)
        
        Area1_label = Label(Entry_frame, text = 'Part 1 [cm\u00B2]:')
        Area1_label.grid(sticky=W, row=1, column=0, padx=5, pady=2)

        Area1_entry = Entry(Entry_frame, width=6)
        Area1_entry.grid(sticky=W, row=1, column=1,  padx=5, pady=2)
        
        Area2_label = Label(Entry_frame, text = 'Part 2 [cm\u00B2]:')
        Area2_label.grid(sticky=W, row=2, column=0, padx=5, pady=2)

        Area2_entry = Entry(Entry_frame, width=6)
        Area2_entry.grid(sticky=W, row=2, column=1,  padx=5, pady=2)

        # Moment of Inertia for each part

        Moment_label = Label(Entry_frame, text = 'Moment of inertia for each part:')
        Moment_label.grid(sticky=W, row=3, column=0, columnspan=5, padx=5, pady=5)
        
        Moment1_label = Label(Entry_frame, text = 'Part 1 [cm\u2074]:')
        Moment1_label.grid(sticky=W, row=4, column=0, padx=5, pady=2)

        Moment1_entry = Entry(Entry_frame, width=6)
        Moment1_entry.grid(sticky=W, row=4, column=1,  padx=5, pady=2)
        
        Moment2_label = Label(Entry_frame, text = 'Part 2 [cm\u2074]:')
        Moment2_label.grid(sticky=W, row=5, column=0, padx=5, pady=2)

        Moment2_entry = Entry(Entry_frame, width=6)
        Moment2_entry.grid(sticky=W, row=5, column=1,  padx=5, pady=2)

        # Distances

        Distance_label = Label(Entry_frame, text = 'Distances according to the drawing (d1, d2, d3):')
        Distance_label.grid(sticky=W, row=6, column=0, columnspan=5, padx=5, pady=5)
        
        Distance1_label = Label(Entry_frame, text = 'd1 [cm]:')
        Distance1_label.grid(sticky=W, row=7, column=0, padx=5, pady=2)

        Distance1_entry = Entry(Entry_frame, width=6)
        Distance1_entry.grid(sticky=W, row=7, column=1,  padx=5, pady=2)
        
        Distance2_label = Label(Entry_frame, text = 'd2 [cm]:')
        Distance2_label.grid(sticky=W, row=8, column=0, padx=5, pady=2)

        Distance2_entry = Entry(Entry_frame, width=6)
        Distance2_entry.grid(sticky=W, row=8, column=1,  padx=5, pady=2)

        Distance3_label = Label(Entry_frame, text = 'd3 [cm]:')
        Distance3_label.grid(sticky=W, row=9, column=0, padx=5, pady=2)

        Distance3_entry = Entry(Entry_frame, width=6)
        Distance3_entry.grid(sticky=W, row=9, column=1,  padx=5, pady=2)


        def create_case():
            try:
                area1 = float(Area1_entry.get())
                area2 = float(Area2_entry.get())
                moment1 = float(Moment1_entry.get())
                moment2 = float(Moment2_entry.get())
                d1 = float(Distance1_entry.get())
                d2 = float(Distance2_entry.get())
                d3 = float(Distance3_entry.get())
            except ValueError:
                messagebox.showerror('Wrong Data!', 'Only numbers!')
            young1 = self.youngm1.get()
            young2 = self.youngm2.get()

            case = MomentOfInteria(E1 = young1 ,E2 = young2 ,A1 = area1, A2 = area2, J1 = moment1, J2 = moment2, d1 = d1, d2 = d2, d3 = d3)
            return case


        results_label = Label(self.frame, text = "Moment of Inertia for composite material: ")
        results_label.grid(row=1, column=0, columnspan = 10, padx=5, pady=5)

        def evaluate():
            case = create_case()
            J = case.momentofinteria()
            results_label.config(text='Moment of Inertia for composite material: ' + str(J) + ' cm\u2074')

      
        evaluate = Button(self.frame, text='Calculate!', command = evaluate)
        evaluate.grid(row=2, column=0, columnspan = 10, padx=5, pady=5)


if __name__ == '__main__':
    root = Tk()
    root.title('Moment of Inertia')
    test = MomentOfInertiaGui(root)
    test.frame.grid(row=0, column=0)
    root.mainloop()
