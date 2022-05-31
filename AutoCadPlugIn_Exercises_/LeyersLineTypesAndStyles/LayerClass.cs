using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Geometry;

namespace LeyersLineTypesAndStyles
{
    public class LayerClass
    {
        [CommandMethod("SetLayerToObject")]
        public static void SetLayerToObject()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt;
                bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                BlockTableRecord btr;
                btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                var ln = new Line(new Point3d(0, 0, 0), new Point3d(100, 100, 0));

                // Assign a layer to the Line
                ln.Layer = "Misc";


                btr.AppendEntity(ln);
                trans.AddNewlyCreatedDBObject(ln, true);

                trans.Commit();
            }
            
        }



        [CommandMethod("DeleteLayer")]
        public static void DeleteLayers()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                db.Clayer = lyTab["0"];
                foreach (ObjectId lyID in lyTab)
                {
                    LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                    if (lytr.Name == "Misc")
                    {
                        lytr.UpgradeOpen();

                        //
                        lytr.Erase(true);
                    }
                    else
                    {
                        doc.Editor.WriteMessage("Skipping Layer [" + lytr.Name + "]");
                    }
                }

                trans.Commit();
            }
        }


        [CommandMethod("OnOffLayers")]
        public static void OnOffLayers()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                db.Clayer = lyTab["0"];
                foreach (ObjectId lyID in lyTab)
                {
                    LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                    if(lytr.Name == "Misc")
                    {
                        lytr.UpgradeOpen();

                        //
                        lytr.IsOff = true;
                        lytr.IsFrozen = true;
                    }
                    else
                    {
                        doc.Editor.WriteMessage("Skipping Layer [" + lytr.Name + "]");
                    }
                }

                trans.Commit();
            }
        }



        [CommandMethod("UpdateLayers")]
        public static void UpdateLayers()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;   
                    foreach (ObjectId lyId in lyTab)
                    {
                        LayerTableRecord lytr = trans.GetObject(lyId, OpenMode.ForRead) as LayerTableRecord;
                        if (lytr.Name == "Misc")
                        {
                            lytr.UpgradeOpen();
                            lytr.Color = Color.FromColorIndex(ColorMethod.ByLayer, 2);

                            LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                            if (ltTab.Has("Hidden") == true)
                            {
                                lytr.LinetypeObjectId = ltTab["Hidden"];
                            }
                            doc.Editor.WriteMessage("Updating Layer: " + lytr.Name);
                            break;
                        }
                        else
                        {
                            doc.Editor.WriteMessage("Skipping Layer [" + lytr.Name + "]");
                        }
                    }


                }
                catch (System.Exception ex)
                {

                    throw;
                }

                trans.Commit();
            }
        }




        [CommandMethod("CreateLayers")]
        public static void CreateLayers()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                if (lyTab.Has("Misc"))
                {
                    doc.Editor.WriteMessage("Layer Exist!");
                    trans.Abort();
                }
                else
                {
                    lyTab.UpgradeOpen();
                    LayerTableRecord ltr = new LayerTableRecord();
                    ltr.Name = "Misc";
                    ltr.Color = Color.FromColorIndex(ColorMethod.ByLayer, 1);
                    lyTab.Add(ltr);
                    trans.AddNewlyCreatedDBObject(ltr, true);

                    db.Clayer = lyTab["Misc"];

                    doc.Editor.WriteMessage("Layer [" + ltr.Name + "] was created successfully.");
                }

                trans.Commit();
            }
        }


        [CommandMethod("ListLayers")]
        public static void ListLayers()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable lyTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                foreach(ObjectId lyID in lyTab)
                {
                    LayerTableRecord lytr = trans.GetObject(lyID, OpenMode.ForRead) as LayerTableRecord;
                    doc.Editor.WriteMessage("\nLayer name: " + lytr.Name);
                }

                trans.Commit();
            }
        }
    }
}
