using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace AutoCAD_manipulate_01
{
    public class AutoCAD_manipulate_01
    {

        [CommandMethod("ScaleObject")]
        public static void ScaleObject()
        {
            //Get the current document
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            //Using transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    //Open the Block Table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    //Open the Block Table recoord ModelSpace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Create
                    using (Polyline pl = new Polyline())
                    {
                        pl.AddVertexAt(0, new Point2d(2, 4), 0, 0, 0);
                        pl.AddVertexAt(1, new Point2d(4, 2), 0, 0, 0);
                        pl.AddVertexAt(2, new Point2d(6, 4), 0, 0, 0);

                        //Close polyline
                        pl.Closed = true;

                        //Adding new object
                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);

                        Polyline plScale = pl.Clone() as Polyline;

                       
                        //Scale
                        plScale.TransformBy(Matrix3d.Scaling(0.5, new Point3d(0,0,0)));

                        //Adding new object
                        btr.AppendEntity(plScale);
                        trans.AddNewlyCreatedDBObject(plScale, true);
                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage(ex.ToString());
                    trans.Abort();
                }
            }
        }


        [CommandMethod("RotateObject")]
        public static void RotateObject()
        {
            //Get the current document
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            //Using transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    //Open the Block Table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    //Open the Block Table recoord ModelSpace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Create
                    using (Polyline pl = new Polyline())
                    {
                        pl.AddVertexAt(0, new Point2d(2, 4), 0, 0, 0);
                        pl.AddVertexAt(1, new Point2d(4, 2), 0, 0, 0);
                        pl.AddVertexAt(2, new Point2d(6, 4), 0, 0, 0);

                        //Close polyline
                        pl.Closed = true;

                        //Adding new object
                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);

                        Polyline plRotate = pl.Clone() as Polyline;

                        Matrix3d curUCSMatrix = doc.Editor.CurrentUserCoordinateSystem;
                        CoordinateSystem3d curUCS = curUCSMatrix.CoordinateSystem3d;

                        //Rotate polyline 45 degree around the z-axis
                        plRotate.TransformBy(Matrix3d.Rotation(0.7854, curUCS.Zaxis, new Point3d(0, 0, 0)));

                        //Adding new object
                        btr.AppendEntity(plRotate);
                        trans.AddNewlyCreatedDBObject(plRotate, true);
                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage(ex.ToString());
                    trans.Abort();
                }
            }
        }



        [CommandMethod("MirrorObject")]
        public static void MirrorObject()
        {
            //Get the current document
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            //Using transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    //Open the Block Table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    //Open the Block Table recoord ModelSpace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Create
                    using (Line ln = new Line())
                    {
                        ln.StartPoint = new Point3d(0, 0, 0);
                        ln.EndPoint = new Point3d(0, 10, 0);

                        //Add 
                        btr.AppendEntity(ln);
                        trans.AddNewlyCreatedDBObject(ln, true);

                        //Define mirror line 
                        Line lnMirror = ln.Clone() as Line;
                        lnMirror.ColorIndex = 5;

                        //Define a mirror line 
                        Line3d lnM = new Line3d(new Point3d(2, 0, 0), new Point3d(2, 2, 0));

                        //Mirroring
                        lnMirror.TransformBy(Matrix3d.Mirroring(lnM));

                        //Add 
                        btr.AppendEntity(lnMirror);
                        trans.AddNewlyCreatedDBObject(lnMirror, true);
                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage(ex.ToString());
                    trans.Abort();
                }
            }
        }

        [CommandMethod("MoveObject")]
        public static void MoveObject()
        {
            //Get the current document
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            //Using transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    //Open the Block Table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    //Open the Block Table recoord ModelSpace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Create
                    using (Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(2, 2, 0);
                        c1.Radius = 0.5;

                        //Create matrix to move the circle using a vector
                        Point3d startPt = new Point3d(0, 0, 0);   
                        Vector3d destVector = startPt.GetVectorTo(new Point3d(2, 0, 0));

                        c1.TransformBy(Matrix3d.Displacement(destVector));

                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);

                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage(ex.ToString());
                    trans.Abort();
                }
            }
        }



        [CommandMethod("EraseObject")]
        public static void EraceObject()
        {
            //Get the current document
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            //Using transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    //Open the Block Table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    //Open the Block Table recoord ModelSpace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Create plyline
                    using (Polyline pl = new Polyline())
                    {
                        pl.AddVertexAt(0,new Point2d(2, 4), 0, 0 ,0);
                        pl.AddVertexAt(1, new Point2d(4, 2), 0, 0, 0);
                        pl.AddVertexAt(2, new Point2d(6, 4), 0, 0, 0);

                        //Adding new object
                        btr.AppendEntity(pl);
                        trans.AddNewlyCreatedDBObject(pl, true);

                        doc.SendStringToExecute("._zoom e ", false, false, false);

                        //Update and display and alert message
                        doc.Editor.Regen();
                        Application.ShowAlertDialog("Erase the newly added polyline");

                        //Erasing polyline
                        pl.Erase(true);
                    }
                    trans.Commit();

                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage(ex.ToString());
                    trans.Abort();
                }
            }
        }


        [CommandMethod("MultipleCopy")]
        public static void MultipleCopy()
        {
            //Get the current document
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            //Using transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    //Open the Block Table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    //Open the Block Table recoord ModelSpace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Create
                    using (Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(0, 0, 0);
                        c1.Radius = 5;

                        //Adding circle
                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);
                       
                        using (Circle c2 = new Circle())
                        {
                            c2.Center = new Point3d(0, 0, 0);
                            c2.Radius = 10;

                            //Adding circle
                            btr.AppendEntity(c2);
                            trans.AddNewlyCreatedDBObject(c2, true);

                            DBObjectCollection col = new DBObjectCollection();
                            col.Add(c1);
                            col.Add(c2);

                            foreach(Entity acEnt in col)
                            {
                                Entity ent;
                                ent = acEnt.Clone() as Entity;
                                ent.ColorIndex = 1;

                                //Create matrix and move each copied entity 20 units to the right
                                ent.TransformBy(Matrix3d.Displacement(new Vector3d(20, 0, 0)));

                                btr.AppendEntity(ent);
                                trans.AddNewlyCreatedDBObject(ent, true);  
                            }
                            trans.Commit();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage(ex.ToString());
                    trans.Abort();
                }
            }
        }





        [CommandMethod("SingleCopy")]
        public static void SingleCopy()
        {
            //Get the current document
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            //Using transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    //Open the Block Table for read
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    //Open the Block Table recoord ModelSpace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    //Create
                    using(Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(2, 3, 0);
                        c1.Radius = 4.25;

                        //Adding circle
                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);
                        
                        //Creating copy
                        Circle c1Clone = c1.Clone() as Circle;
                        c1Clone.Radius = 1;

                        //Adding circle
                        btr.AppendEntity(c1Clone);
                        trans.AddNewlyCreatedDBObject(c1Clone, true);
                    }
                    trans.Commit();

                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage(ex.ToString());
                    trans.Abort();
                }
            }           
        }
    }
}
