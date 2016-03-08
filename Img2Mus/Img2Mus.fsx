open System.Drawing
open System.Windows
open System.Windows.Forms

let frm = new Forms.Form(Width=1024,Height=1024)
frm.Show()
let wrapPanel=new Forms.FlowLayoutPanel() 
wrapPanel.Size <- frm.Size
frm.Controls.Add(wrapPanel)
let addToWrap filename =
    let box = new Forms.PictureBox()
    let bmp = new Bitmap(filename:string)
    let size = new Size(bmp.Width, bmp.Height)
    box.ClientSize <- size
    box.Image <- ( bmp :> Image)
    wrapPanel.Controls.Add box
    box,bmp
let picture1,bmp1 = addToWrap """C:\Users\KELLERD\Desktop\RedactionError.png"""
let picture2,bmp2 = addToWrap """C:\Users\KELLERD\Desktop\RedactionError.png"""
type data = {x:int;y:int;value:Color;width:int}
let patternSeq f (bmp:Bitmap) =
    seq {for x in 0 .. bmp.Width - 1 do
            for y in 0 .. bmp.Height - 1 do
                         yield f(x,y,bmp) 
         }
let avgColor (c:seq<Color>) = 
    Color.FromArgb(
        c |> 
        Seq.averageBy (fun cl -> float (cl.ToArgb())) |> 
        int)
let data width bmp = patternSeq (fun (x,y,bmp) -> {x=x;y=y;value=bmp.GetPixel(x,y);width=width}) bmp
                    |> Seq.groupBy (fun d -> d.x/width,d.y/width)
                    |> Seq.map (fun ((x,y),d) -> {x=x;y=y;value=(d|>Seq.map(fun data -> data.value) |> avgColor );width=(d|>Seq.head|>(fun data -> data.width))})
                    |> Seq.cache 

let x = data 100 bmp1 |> Seq.toList 