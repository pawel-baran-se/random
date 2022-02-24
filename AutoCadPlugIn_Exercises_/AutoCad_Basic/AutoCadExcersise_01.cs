using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;


namespace HelloAutoCAD_01
{
    public class AutoCadExcersise_01
    {
        [CommandMethod("DrawPline")]
        public void DrawPline()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    edt.WriteMessage("Drawing a Polyline!");
                    BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Polyline
                    var pl = new Polyline();
                    for (int i = 0; i < 6; i++)
                    {
                        pl.AddVertexAt(i, new Point2d(i * 10, i * 10), 0, 0, 0);
                    }


                    btr.AppendEntity(pl);
                    trans.AddNewlyCreatedDBObject(pl, true);

                    

                    trans.Commit();

                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error encounted" + ex.Message);
                    trans.Abort();
                    throw;
                }
            }

        }


        [CommandMethod("DrawArc")]
        public void DrawArc()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    edt.WriteMessage("Drawing a Arc!");
                    BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Arc
                    Point3d centerPt = new Point3d(300, 300, 0);
                    double arcRad = 6;
                    double angleFirs = 1.117;
                    double angleSec = 3.5605;
                    using (Arc arc = new Arc())
                    {
                        arc.Center = centerPt;
                        arc.StartAngle = angleFirs;
                        arc.EndAngle = angleSec;
                        arc.Radius = arcRad;

                        btr.AppendEntity(arc);
                        trans.AddNewlyCreatedDBObject(arc, true);

                    }

                    trans.Commit();

                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error encounted" + ex.Message);
                    trans.Abort();
                    throw;
                }
            }

        }

        [CommandMethod("DrawCircle")]
        public void DrawCircle()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    edt.WriteMessage("Drawing a Circle!");
                    BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Circle
                    Point3d centerPt = new Point3d(100, 100, 0);
                    double cicrcleRad = 100.0;
                    using (Circle circle = new Circle())
                    {
                        circle.Radius = cicrcleRad;
                        circle.Center = centerPt;

                        btr.AppendEntity(circle);
                        trans.AddNewlyCreatedDBObject(circle, true);

                    }                  

                    trans.Commit();

                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error encounted" + ex.Message);
                    trans.Abort();
                    throw;
                }
            }

        }



        [CommandMethod("DrawMText")]
        public void DrawMText()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using(Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    edt.WriteMessage("Drawing Mtext");                   
                    BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    string txt = "Hello from C#!";
                    Point3d insPt = new Point3d(200, 200, 0);
                    using (MText mtx = new MText())
                    {
                        mtx.Contents = txt;
                        mtx.Location = insPt;

                        btr.AppendEntity(mtx);
                        trans.AddNewlyCreatedDBObject(mtx, true);
                    }
                    trans.Commit();

                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error encounted" + ex.Message);
                    trans.Abort();
                    throw;
                }
            }

        }


        [CommandMethod("DrawLine")]
        public void DrawLine()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            using(Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    //Sends a message
                    edt.WriteMessage("Creating a line ");

                    Point3d pt1 = new Point3d(0, 0, 0);
                    Point3d pt2 = new Point3d(100, 100, 0);
                    Line ln = new Line(pt1, pt2);
                    ln.ColorIndex = 1; //Color is red
                    btr.AppendEntity(ln);
                    trans.AddNewlyCreatedDBObject(ln, true);
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    edt.WriteMessage("Error " + ex.Message);
                    trans.Abort();
                }
            }
        }



        [CommandMethod("HelloWorld")]
        public void HelloAutoCADFromCSharp()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor edt = doc.Editor;

            edt.WriteMessage("Hello AutoCAD!");
        }

        [CommandMethod("SayHi")]
        public void SayHi()
        {
            Application.ShowAlertDialog("Hello AutoCad from C#2");
        }


    }
}