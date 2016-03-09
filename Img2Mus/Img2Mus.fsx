open System.Collections.Generic
open NAudio.Midi
open System.Drawing
open System.Windows
open System.Windows.Forms
open System

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
    box
let create (box:PictureBox) =
    let box2 = new Forms.PictureBox()
    box.ClientSize <- box.ClientSize
    wrapPanel.Controls.Add box2
    box2

let picture1 = addToWrap """C:\Users\KELLERD\Desktop\RedactionError.png"""
let picture2 = create picture1

type Data = {X:int;Y:int;Value:Color;Width:int}
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
let data width bmp = patternSeq (fun (x,y,bmp) -> {X=x;Y=y;Value=bmp.GetPixel(x,y);Width=width}) bmp
                    |> Seq.groupBy (fun d -> d.X/width,d.Y/width)
                    |> Seq.map (fun ((x,y),d) -> {X=x;Y=y;Value=(d|>Seq.map(fun data -> data.Value) |> avgColor );Width=(d|>Seq.head|>(fun data -> data.Width))})
                    |> Seq.cache 
//let setPixelDelegate (p:PictureBox) =
//    let graphics = p.CreateGraphics()
//    let bmp = p.Image :?> Bitmap
//    let setPixel data =
//        use pixel = new Bitmap(1,1)
//        pixel.SetPixel(0,0,data.Value) |> ignore
//        let rect = new Rectangle(data.X,data.Y,data.Width,data.Width)
//        graphics.DrawImageUnscaled(pixel,rect) |> ignore
//        bmp.SetPixel(data.X,data.Y,data.Value) |> ignore
//    p,new Action<Data>(setPixel)
//let mailbox (p:PictureBox,del) = new MailboxProcessor<Data>(fun inbox ->
//    let rec loop() = 
//        async {
//            let! msg = inbox.Receive()
//            p.Invoke(del,msg) |> ignore       
//            do! loop()
//        }
//    loop())
//
//let mailboxinst = setPixelDelegate picture2 |> mailbox 
//mailboxinst.Start()
//data 2 (picture1.Image :?> Bitmap) |> Seq.iter mailboxinst.Post
type Note = {Key:int;Name:string;Frequency:float}
let piano = [{Key=88; Name="C8 Eighth octave"; Frequency=4186.01  }   ;
            {Key=87; Name="B7"; Frequency=3951.07                       }        ;
            {Key=86; Name="A♯7/B♭7"; Frequency=3729.31                 }        ;
            {Key=85; Name="A7"; Frequency=3520.0                          }        ;
            {Key=84; Name="G♯7/A♭7"; Frequency=3322.44                 }        ;
            {Key=83; Name="G7"; Frequency=3135.96                       }        ;
            {Key=82; Name="F♯7/G♭7"; Frequency=2959.96                 }        ;
            {Key=81; Name="F7"; Frequency=2793.83                       }        ;
            {Key=80; Name="E7"; Frequency=2637.02                       }        ;
            {Key=79; Name="D♯7/E♭7"; Frequency=2489.02                 }        ;
            {Key=78; Name="D7"; Frequency=2349.32                       }        ;
            {Key=77; Name="C♯7/D♭7"; Frequency=2217.46                 }        ;
            {Key=76; Name="C7 Double high C"; Frequency=2093.0            }        ;
            {Key=75; Name="B6"; Frequency=1975.53                       }        ;
            {Key=74; Name="A♯6/B♭6"; Frequency=1864.66                 }        ;
            {Key=73; Name="A6"; Frequency=1760.0                          }        ;
            {Key=72; Name="G♯6/A♭6"; Frequency=1661.22                 }        ;
            {Key=71; Name="G6"; Frequency=1567.98                       }        ;
            {Key=70; Name="F♯6/G♭6"; Frequency=1479.98                 }        ;
            {Key=69; Name="F6"; Frequency=1396.91                       }        ;
            {Key=68; Name="E6"; Frequency=1318.51                       }        ;
            {Key=67; Name="D♯6/E♭6"; Frequency=1244.51                 }        ;
            {Key=66; Name="D6"; Frequency=1174.66                       }        ;
            {Key=65; Name="C♯6/D♭6"; Frequency=1108.73                 }        ;
            {Key=64; Name="C6 Soprano C (High C)"; Frequency=1046.5     }        ;
            {Key=63; Name="B5"; Frequency=987.767                       }        ;
            {Key=62; Name="A♯5/B♭5"; Frequency=932.328                 }        ;
            {Key=61; Name="A5"; Frequency=880.0                           }        ;
            {Key=60; Name="G♯5/A♭5"; Frequency=830.609                 }        ;
            {Key=59; Name="G5"; Frequency=783.991                       }        ;
            {Key=58; Name="F♯5/G♭5"; Frequency=739.989                 }        ;
            {Key=57; Name="F5"; Frequency=698.456                       }        ;
            {Key=56; Name="E5"; Frequency=659.255                       }        ;
            {Key=55; Name="D♯5/E♭5"; Frequency=622.254                 }        ;
            {Key=54; Name="D5"; Frequency=587.33                        }        ;
            {Key=53; Name="C♯5/D♭5"; Frequency=554.365                 }        ;
            {Key=52; Name="C5 Tenor C"; Frequency=523.251               }        ;
            {Key=51; Name="B4"; Frequency=493.883                       }        ;
            {Key=50; Name="A♯4/B♭4"; Frequency=466.164                 }        ;
            {Key=49; Name="A4 A440"; Frequency=440.0                      }        ;
            {Key=48; Name="G♯4/A♭4"; Frequency=415.305                 }        ;
            {Key=47; Name="G4"; Frequency=391.995                       }        ;
            {Key=46; Name="F♯4/G♭4"; Frequency=369.994                 }        ;
            {Key=45; Name="F4"; Frequency=349.228                       }        ;
            {Key=44; Name="E4"; Frequency=329.628                       }        ;
            {Key=43; Name="D♯4/E♭4"; Frequency=311.127                 }        ;
            {Key=42; Name="D4"; Frequency=293.665                       }        ;
            {Key=41; Name="C♯4/D♭4"; Frequency=277.183                 }        ;
            {Key=40; Name="C4 Middle C"; Frequency=261.626              }        ;
            {Key=39; Name="B3"; Frequency=246.942                       }        ;
            {Key=38; Name="A♯3/B♭3"; Frequency=233.082                 }        ;
            {Key=37; Name="A3"; Frequency=220.0                           }        ;
            {Key=36; Name="G♯3/A♭3"; Frequency=207.652                 }        ;
            {Key=35; Name="G3"; Frequency=195.998                       }        ;
            {Key=34; Name="F♯3/G♭3"; Frequency=184.997                 }        ;
            {Key=33; Name="F3"; Frequency=174.614                       }        ;
            {Key=32; Name="E3"; Frequency=164.814                       }        ;
            {Key=31; Name="D♯3/E♭3"; Frequency=155.563                 }        ;
            {Key=30; Name="D3"; Frequency=146.832                       }        ;
            {Key=29; Name="C♯3/D♭3"; Frequency=138.591                 }        ;
            {Key=28; Name="C3"; Frequency=130.813                       }        ;
            {Key=27; Name="B2"; Frequency=123.471                       }        ;
            {Key=26; Name="A♯2/B♭2"; Frequency=116.541                 }        ;
            {Key=25; Name="A2"; Frequency=110.0                           }        ;
            {Key=24; Name="G♯2/A♭2"; Frequency=103.826                 }        ;
            {Key=23; Name="G2"; Frequency=97.9989                       }        ;
            {Key=22; Name="F♯2/G♭2"; Frequency=92.4986                 }        ;
            {Key=21; Name="F2"; Frequency=87.3071                       }        ;
            {Key=20; Name="E2"; Frequency=82.4069                       }        ;
            {Key=19; Name="D♯2/E♭2"; Frequency=77.7817                 }        ;
            {Key=18; Name="D2"; Frequency=73.4162                       }        ;
            {Key=17; Name="C♯2/D♭2"; Frequency=69.2957                 }        ;
            {Key=16; Name="C2 Deep C"; Frequency=65.4064                }        ;
            {Key=15; Name="B1"; Frequency=61.7354                       }        ;
            {Key=14; Name="A♯1/B♭1"; Frequency=58.2705                 }        ;
            {Key=13; Name="A1"; Frequency=55.0                            }        ;
            {Key=12; Name="G♯1/A♭1"; Frequency=51.9131                 }        ;
            {Key=11; Name="G1"; Frequency=48.9994                       }        ;
            {Key=10; Name="F♯1/G♭1"; Frequency=46.2493                 }        ;
            {Key=9; Name="F1"; Frequency=43.6535                        }        ;
            {Key=8; Name="E1"; Frequency=41.2034                        }        ;
            {Key=7; Name="D♯1/E♭1"; Frequency=38.8909                  }        ;
            {Key=6; Name="D1"; Frequency=36.7081                        }        ;
            {Key=5; Name="C♯1/D♭1"; Frequency=34.6478                  }        ;
            {Key=4; Name="C1 Pedal C"; Frequency=32.7032                }        ;
            {Key=3; Name="B0"; Frequency=30.8677                        }        ;
            {Key=2; Name="A♯0/B♭0"; Frequency=29.1352                  }        ;
            {Key=1; Name="A0 Double Pedal A"; Frequency=27.5            }] |> List.map (fun x -> x.Key, x) |> Map.ofList
let imageToNotes d = 
    let value = d.Value
    let amplitude = float (value.GetBrightness()) / 3. + (1./3.)
    let duration = float (value.GetSaturation()) 
    let note = float (value.GetHue() / float32 360.0) * float piano.Count |> Math.Round |> int
    amplitude, duration, piano.Item note
let x = data 100 (picture1.Image :?> Bitmap) |> Seq.map imageToNotes 
#r """C:\Users\KELLERD\Documents\Visual Studio 2013\Projects\Img2Mus\packages\NAudio.1.7.3\lib\net35\NAudio.dll"""





let TimeBetween = 100L; // 100 ms in on the track
let Channel = 1; // Channel needs to be between 1 and 16
let NoteNumber = 54;
let Velocity = 127; // Velocity is from 0 which is considered off, to 127 which is the maximum
let Duration = 100; 
let DeltaTicksPerQuarterNote  = 120;
let FileType = 1;

let note1On noteNumber duration time = new  NoteOnEvent(time, Channel, noteNumber, Velocity, duration)
let note1Off noteNumber duration time = new NoteOnEvent(time + int64(duration), Channel, noteNumber, 0, 0)
let events = new   MidiEventCollection(FileType, DeltaTicksPerQuarterNote)
let makeTrack (xss:MidiEventCollection) xs = 
    let appendEndMarker (eventList:IList<MidiEvent>) =
        let absoluteTime = 
            if (eventList.Count > 0) then
                eventList.Item(eventList.Count - 1).AbsoluteTime
            else
                0L
        eventList.Add(new MetaEvent(MetaEventType.EndTrack, 0, absoluteTime))
    let track = xss.AddTrack()
    xs |> List.iter (fun x -> track.Add(x))
    appendEndMarker track
    track

let notes = [40;40;47;47;49;49;47] |> List.mapi (fun n x -> n,x) |> List.collect (fun (time,note) -> [note1On note Duration (int64(time) * TimeBetween);note1Off note Duration (int64(time) * TimeBetween)])
//let notes = Map.toList piano |> List.mapi (fun n x -> n,x) |> List.collect (fun (time,(note,_)) -> [note1On note Duration (int64(time) * TimeBetween);note1Off note Duration (int64(time) * TimeBetween)])
makeTrack events (notes)
MidiFile.Export("""C:\Users\KELLERD\Desktop\test.midi""", events)