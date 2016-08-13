using Gwen.Control;
using static System.Environment;
using System.Drawing;
using System.Threading;
using System;
using SFML.Graphics;

namespace Editor.Classes
{
    class GUI
    {
        //GUI
        Canvas mapeditorUI;
        Gwen.Font srvrFont;
        Gwen.Renderer.SFML mapRenderer;
        Map editorMap;
        public NPC editorNpc;
        //Map editor variables
        public int editMode;
        public int editLayer;
        public int editTileset;
        public int editType;
        public bool editGrid;
        public bool editshowTypes;
        public int edittileX;
        public int edittileY;
        public int edittileH;
        public int edittileW;
        public int brushSize;
        public int editselectNpc;
        public const int picX = 32;
        public const int picY = 32;
        //Map tool window
        public WindowControl maptoolsWin;
        Button modeBtn;
        Button layerBtn;
        Button typeBtn;
        Button nexttilesetBtn;
        Button prevtilesetBtn;
        Button savemapBtn;
        Button loadmapBtn;
        Button fillLayer;
        Button deleteLayer;
        //Map mode window
        public WindowControl modeWin;
        RadioButtonGroup modeGroup;
        LabeledCheckBox chkGrid;
        LabeledCheckBox chkshowType;
        //Map layer window
        public WindowControl layerWin;
        RadioButtonGroup layerGroup;
        //Map type window
        public WindowControl typeWin;
        RadioButtonGroup typeGroup;
        //Paint multiple tiles
        public WindowControl paintWin;
        RadioButtonGroup paintGroup;
        //Npc editor
        public WindowControl npcWin;
        //Name controls
        Label nameLabel;
        TextBox nameText;
        //Directon controls
        Label dirLabel;
        HorizontalSlider dirScroll;
        //Sprite controls
        Label spriteLabel;
        ImagePanel spriteSelect;
        HorizontalSlider spriteScroll;
        ComboBox behaviorCombo;
        Label behaviorLabel;
        HorizontalSlider spawntimeScroll;
        Label spawntimeLabel;
        //npc editor tool window
        public WindowControl npctoolWin;
        Button npcSave;
        Button npcOpen;
        //npc spawn tile selection
        public WindowControl npcSelect;
        Label npcLabel;
        public HorizontalSlider npcNum;

        public GUI(Canvas svrCanvas, Gwen.Font svrFont, Gwen.Renderer.SFML gwenRenderer, Map editMap)
        {
            mapeditorUI = svrCanvas;
            srvrFont = svrFont;
            mapRenderer = gwenRenderer;
            editorMap = editMap;
        }

        public void CreateToolWindow(Base parent)
        {
            maptoolsWin = new WindowControl(parent.GetCanvas());
            maptoolsWin.Title = "Tools";
            maptoolsWin.SetBounds(new Rectangle(950, 235, 200, 325));
            maptoolsWin.IsClosable = false;
            maptoolsWin.DisableResizing();

            modeBtn = new Button(maptoolsWin);
            modeBtn.SetBounds(new Rectangle(45, 15, 100, 25));
            modeBtn.Text = "Change Mode";
            modeBtn.Clicked += ClickChangeModeButton;

            layerBtn = new Button(maptoolsWin);
            layerBtn.SetBounds(new Rectangle(45, 45, 100, 25));
            layerBtn.Text = "Change Layer";
            layerBtn.Clicked += ClickLayerButton;

            typeBtn = new Button(maptoolsWin);
            typeBtn.SetBounds(new Rectangle(45, 75, 100, 25));
            typeBtn.Text = "Change Type";
            typeBtn.Clicked += ClickTypeButton;

            nexttilesetBtn = new Button(maptoolsWin);
            nexttilesetBtn.SetBounds(new Rectangle(45, 105, 100, 25));
            nexttilesetBtn.Text = "Next Tileset";
            nexttilesetBtn.Clicked += ClickNextTilesetButton;

            prevtilesetBtn = new Button(maptoolsWin);
            prevtilesetBtn.SetBounds(new Rectangle(45, 135, 100, 25));
            prevtilesetBtn.Text = "Prev Tileset";
            prevtilesetBtn.Clicked += ClickPrevTilesetButton;

            fillLayer = new Button(maptoolsWin);
            fillLayer.SetBounds(new Rectangle(45, 165, 100, 25));
            fillLayer.Text = "Fill Layer";
            fillLayer.DoubleClicked += FillLayer_DoubleClicked;

            deleteLayer = new Button(maptoolsWin);
            deleteLayer.SetBounds(new Rectangle(45, 195, 100, 25));
            deleteLayer.Text = "Delete Layer";
            deleteLayer.DoubleClicked += DeleteLayer_DoubleClicked;

            savemapBtn = new Button(maptoolsWin);
            savemapBtn.SetBounds(new Rectangle(45, 225, 100, 25));
            savemapBtn.Text = "Save Map";
            savemapBtn.Clicked += SavemapBtn_Clicked;

            loadmapBtn = new Button(maptoolsWin);
            loadmapBtn.SetBounds(new Rectangle(45, 255, 100, 25));
            loadmapBtn.Text = "Load Map";
            loadmapBtn.Clicked += LoadmapBtn_Clicked;
        }

        private void LoadmapBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            editorMap.LoadMap();
            Console.WriteLine("Loading Map...");
        }

        private void DeleteLayer_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    switch (editLayer)
                    {
                        case (int)TileLayers.Ground:
                            editorMap.Ground[x, y].tileX = 0;
                            editorMap.Ground[x, y].tileY = 0;
                            editorMap.Ground[x, y].tileW = 0;
                            editorMap.Ground[x, y].tileH = 0;
                            editorMap.Ground[x, y].Tileset = 0;
                            editorMap.Ground[x, y].type = 0;
                            break;
                        case (int)TileLayers.Mask:
                            editorMap.Mask[x, y].tileX = 0;
                            editorMap.Mask[x, y].tileY = 0;
                            editorMap.Mask[x, y].tileW = 0;
                            editorMap.Mask[x, y].tileH = 0;
                            editorMap.Mask[x, y].Tileset = 0;
                            editorMap.Mask[x, y].type = 0;
                            break;
                        case (int)TileLayers.MaskA:
                            editorMap.MaskA[x, y].tileX = 0;
                            editorMap.MaskA[x, y].tileY = 0;
                            editorMap.MaskA[x, y].tileW = 0;
                            editorMap.MaskA[x, y].tileH = 0;
                            editorMap.MaskA[x, y].Tileset = 0;
                            editorMap.MaskA[x, y].type = 0;
                            break;
                        case (int)TileLayers.Fringe:
                            editorMap.Fringe[x, y].tileX = 0;
                            editorMap.Fringe[x, y].tileY = 0;
                            editorMap.Fringe[x, y].tileW = 0;
                            editorMap.Fringe[x, y].tileH = 0;
                            editorMap.Fringe[x, y].Tileset = 0;
                            editorMap.Fringe[x, y].type = 0;
                            break;
                        case (int)TileLayers.FringeA:
                            editorMap.FringeA[x, y].tileX = 0;
                            editorMap.FringeA[x, y].tileY = 0;
                            editorMap.FringeA[x, y].tileW = 0;
                            editorMap.FringeA[x, y].tileH = 0;
                            editorMap.FringeA[x, y].Tileset = 0;
                            editorMap.FringeA[x, y].type = 0;
                            break;
                    }
                }
            }
            Console.WriteLine("Layer deleted!");
        }

        private void FillLayer_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    switch (editLayer)
                    {
                        case (int)TileLayers.Ground:
                            editorMap.Ground[x, y].tileX = edittileX * picX;
                            editorMap.Ground[x, y].tileY = edittileY * picY;
                            editorMap.Ground[x, y].tileW = picX;
                            editorMap.Ground[x, y].tileH = picY;
                            editorMap.Ground[x, y].Tileset = editTileset;
                            editorMap.Ground[x, y].type = editType;
                            break;
                        case (int)TileLayers.Mask:
                            editorMap.Mask[x, y].tileX = edittileX * picX;
                            editorMap.Mask[x, y].tileY = edittileY * picY;
                            editorMap.Mask[x, y].tileW = picX;
                            editorMap.Mask[x, y].tileH = picY;
                            editorMap.Mask[x, y].Tileset = editTileset;
                            editorMap.Mask[x, y].type = editType;
                            break;
                        case (int)TileLayers.MaskA:
                            editorMap.MaskA[x, y].tileX = edittileX * picX;
                            editorMap.MaskA[x, y].tileY = edittileY * picY;
                            editorMap.MaskA[x, y].tileW = picX;
                            editorMap.MaskA[x, y].tileH = picY;
                            editorMap.MaskA[x, y].Tileset = editTileset;
                            editorMap.MaskA[x, y].type = editType;
                            break;
                        case (int)TileLayers.Fringe:
                            editorMap.Fringe[x, y].tileX = edittileX * picX;
                            editorMap.Fringe[x, y].tileY = edittileY * picY;
                            editorMap.Fringe[x, y].tileW = picX;
                            editorMap.Fringe[x, y].tileH = picY;
                            editorMap.Fringe[x, y].Tileset = editTileset;
                            editorMap.Fringe[x, y].type = editType;
                            break;
                        case (int)TileLayers.FringeA:
                            editorMap.FringeA[x, y].tileX = edittileX * picX;
                            editorMap.FringeA[x, y].tileY = edittileY * picY;
                            editorMap.FringeA[x, y].tileW = picX;
                            editorMap.FringeA[x, y].tileH = picY;
                            editorMap.FringeA[x, y].Tileset = editTileset;
                            editorMap.FringeA[x, y].type = editType;
                            break;
                    }
                }
            }
            Console.WriteLine("Layer filled!");
        }

        private void SavemapBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            editorMap.SaveMap();
            Console.WriteLine("Map Saved!");
        }

        public void CreateLayerWindow(Base parent)
        {
            layerWin = new WindowControl(parent.GetCanvas());
            layerWin.Title = "Select Layer";
            layerWin.SetBounds(new Rectangle(740, 235, 200, 200));
            layerWin.DisableResizing();

            layerGroup = new RadioButtonGroup(layerWin);
            layerGroup.SetPosition(60, 25);
            layerGroup.Text = "Select Layer";
            layerGroup.AddOption(" Ground").Select();
            layerGroup.AddOption(" Mask");
            layerGroup.AddOption(" MaskA");
            layerGroup.AddOption(" Fringe");
            layerGroup.AddOption(" FringeA");
            layerGroup.SelectionChanged += LayerGroup_SelectionChanged;
        }

        public void CreateTypeWindow(Base parent)
        {
            typeWin = new WindowControl(parent.GetCanvas());
            typeWin.Title = "Select Type";
            typeWin.SetBounds(new Rectangle(950, 25, 200, 200));
            typeWin.DisableResizing();

            typeGroup = new RadioButtonGroup(typeWin);
            typeGroup.SetPosition(60, 25);
            typeGroup.Text = "Select Type";
            typeGroup.AddOption(" None").Select();
            typeGroup.AddOption(" Blocked");
            typeGroup.AddOption(" NPC Spawn");
            typeGroup.SelectionChanged += TypeGroup_SelectionChanged;
        }

        public void CreateModeWindow(Base parent)
        {
            modeWin = new WindowControl(parent.GetCanvas());
            modeWin.Title = "Select Mode";
            modeWin.SetBounds(new Rectangle(740, 25, 200, 200));
            modeWin.DisableResizing();

            modeGroup = new RadioButtonGroup(modeWin);
            modeGroup.SetPosition(60, 25);
            modeGroup.Text = "Select Mode";
            modeGroup.AddOption(" Layer").Select();
            modeGroup.AddOption(" Type");
            //modeGroup.AddOption(" Tileset");
            modeGroup.SelectionChanged += ModeGroup_SelectionChanged;

            chkGrid = new LabeledCheckBox(modeWin);
            chkGrid.Text = " Grid";
            chkGrid.SetPosition(65, 85);
            chkGrid.Checked += ChkGrid_Checked;
            chkGrid.UnChecked += ChkGrid_UnChecked;

            chkshowType = new LabeledCheckBox(modeWin);
            chkshowType.Text = " Show Types";
            chkshowType.SetPosition(65, 105);
            chkshowType.Checked += ChkshowType_Checked;
            chkshowType.UnChecked += ChkshowType_UnChecked;
        }

        public void CreatePaintWindow(Base parent)
        {
            paintWin = new WindowControl(parent.GetCanvas());
            paintWin.Title = "Paint Brush Size";
            paintWin.SetBounds(new Rectangle(740, 445, 200, 150));
            paintWin.DisableResizing();

            paintGroup = new RadioButtonGroup(paintWin);
            paintGroup.SetPosition(60, 25);
            paintGroup.Text = "Select Size";
            paintGroup.AddOption(" 1x1").Select();
            paintGroup.AddOption(" 5x5");
            paintGroup.AddOption(" 10x10");
            paintGroup.SelectionChanged += PaintGroup_SelectionChanged;
        }

        public void CreateNpcToolWindow(Base parent)
        {
            npctoolWin = new WindowControl(parent.GetCanvas());
            npctoolWin.Title = "Npc Editor Tools";
            npctoolWin.SetBounds(new Rectangle(25, 25, 200, 325));
            npctoolWin.DisableResizing();
            npctoolWin.IsClosable = false;

            npcSave = new Button(npctoolWin);
            npcSave.SetBounds(new Rectangle(45, 15, 100, 25));
            npcSave.Text = "Save";
            npcSave.Clicked += NpcSave_Clicked;

            npcOpen = new Button(npctoolWin);
            npcOpen.SetBounds(new Rectangle(45, 45, 100, 25));
            npcOpen.Text = "Open";
            npcOpen.Clicked += NpcOpen_Clicked;
        }

        public void CreateNpcEditWindow(Base parent)
        {
            npcWin = new WindowControl(parent.GetCanvas());
            npcWin.Title = "Edit Npc";
            npcWin.SetBounds(new Rectangle(400, 100, 400, 400));
            npcWin.DisableResizing();
            npcWin.KeyboardInputEnabled = true;

            nameLabel = new Label(npcWin);
            nameLabel.SetPosition(25, 15);
            nameLabel.SetText("Name:");

            nameText = new TextBox(npcWin);
            nameText.SetPosition(25, 35);
            nameText.SetSize(140, 25);

            dirLabel = new Label(npcWin);
            dirLabel.SetPosition(25, 75);
            dirLabel.SetText("Direction: ?");

            dirScroll = new HorizontalSlider(npcWin);
            dirScroll.SetBounds(new Rectangle(25, 95, 140, 15));
            dirScroll.SetRange(0, 3);
            dirScroll.Value = 0;
            dirScroll.NotchCount = 3;
            dirScroll.SnapToNotches = true;
            dirScroll.ValueChanged += ScrollDir_ValueChanged;

            spriteLabel = new Label(npcWin);
            spriteLabel.SetPosition(25, 135);
            spriteLabel.SetText("Sprite : ?");

            spriteScroll = new HorizontalSlider(npcWin);
            spriteScroll.SetBounds(new Rectangle(25, 155, 140, 15));
            spriteScroll.SetRange(0, 200);
            spriteScroll.Value = 1;
            spriteScroll.NotchCount = 200;
            spriteScroll.SnapToNotches = true;
            spriteScroll.ValueChanged += SpriteScroll_ValueChanged;

            spriteSelect = new ImagePanel(npcWin);
            spriteSelect.SetBounds(new Rectangle(25, 175, 128, 192));

            behaviorLabel = new Label(npcWin);
            behaviorLabel.SetPosition(200, 15);
            behaviorLabel.SetText("Behavior: ");

            behaviorCombo = new ComboBox(npcWin);
            behaviorCombo.SetPosition(200, 35);
            behaviorCombo.Width = 175;
            behaviorCombo.AddItem("Friendly", "fri", 0);
            behaviorCombo.AddItem("Passive", "pas", 1);
            behaviorCombo.AddItem("Aggressive", "agg", 2);
            behaviorCombo.ItemSelected += BehaviorCombo_ItemSelected;

            spawntimeLabel = new Label(npcWin);
            spawntimeLabel.SetPosition(200, 75);
            spawntimeLabel.SetText("Spawn Time (MS): ?");

            spawntimeScroll = new HorizontalSlider(npcWin);
            spawntimeScroll.SetBounds(new Rectangle(200, 95, 140, 15));
            spawntimeScroll.SetRange(0, 100000);
            spawntimeScroll.Value = 1000;
            spawntimeScroll.NotchCount = 100;
            spawntimeScroll.SnapToNotches = true;
            spawntimeScroll.ValueChanged += SpawntimeScroll_ValueChanged;
        }

        private void SpawntimeScroll_ValueChanged(Base sender, EventArgs arguments)
        {
            spawntimeLabel.SetText("Spawn Time (MS): " + spawntimeScroll.Value);
        }

        private void BehaviorCombo_ItemSelected(Base sender, ItemSelectedEventArgs arguments)
        {
            if (behaviorCombo.SelectedItem.Name == "fri")
            {
                editorNpc.Behavior = 0;
            }
            else if (behaviorCombo.SelectedItem.Name == "pas")
            {
                editorNpc.Behavior = 1;
            }
            else
            {
                editorNpc.Behavior = 2;
            }
        }

        private void SpriteScroll_ValueChanged(Base sender, EventArgs arguments)
        {
            spriteLabel.SetText("Sprite: " + spriteScroll.Value);
            spriteSelect.ImageName = "Resources/Characters/" + (spriteScroll.Value) + ".png";
        }

        private void ScrollDir_ValueChanged(Base sender, EventArgs arguments)
        {
            switch ((int)dirScroll.Value)
            {
                case (int)Directions.Down:
                    dirLabel.SetText("Direction: " + dirScroll.Value + " - Down");
                    break;
                case (int)Directions.Left:
                    dirLabel.SetText("Direction: " + dirScroll.Value + " - Left");
                    break;
                case (int)Directions.Right:
                    dirLabel.SetText("Direction: " + dirScroll.Value + " - Right");
                    break;
                case (int)Directions.Up:
                    dirLabel.SetText("Direction: " + dirScroll.Value + " - Up");
                    break;
            }
        }

        public void LoadNpcDataIntoUI()
        {
            if (npcWin == null || editorNpc == null) { return; }

            nameText.SetText(editorNpc.Name);
            dirLabel.SetText("Direction: " + editorNpc.Direction);
            dirScroll.Value = editorNpc.Direction;
            spriteLabel.SetText("Sprite: " + editorNpc.Sprite);
            spriteScroll.Value = editorNpc.Sprite;
            spawntimeLabel.SetText("Spawn Time (MS): " + editorNpc.SpawnTime);
            spawntimeScroll.Value = editorNpc.SpawnTime;
            behaviorCombo.SelectByUserData(editorNpc.Behavior);
        }

        public void SaveNpcDataFromUI()
        {
            editorNpc.Name = nameText.Text;
            editorNpc.Direction = (int)dirScroll.Value;
            editorNpc.Sprite = (int)spriteScroll.Value;
            editorNpc.SpawnTime = (int)spawntimeScroll.Value;
            editorNpc.SaveNPC();
            npcWin.Close();
        }

        private void NpcOpen_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (editorNpc == null) { return; }

            editorNpc.LoadNPC();
            if (npcWin.IsVisible != true) { CreateNpcEditWindow(sender.GetCanvas()); }
            LoadNpcDataIntoUI();
        }

        private void NpcSave_Clicked(Base sender, EventArgs arguments)
        {
            if (editorNpc == null) { return; }

            SaveNpcDataFromUI();
        }

        private void PaintGroup_SelectionChanged(Base sender, ItemSelectedEventArgs arguments)
        {
            RadioButtonGroup paint = sender as RadioButtonGroup;
            LabeledRadioButton select = paint.Selected;

            if (select.Text == " 1x1")
            {
                brushSize = 1;
            }
            if (select.Text == " 5x5")
            {
                brushSize = 5;
            }
            if (select.Text == " 10x10")
            {
                brushSize = 10;
            }
        }

        private void ChkshowType_UnChecked(Base sender, EventArgs arguments)
        {
            editshowTypes = false;
        }

        private void ChkshowType_Checked(Base sender, EventArgs arguments)
        {
            editshowTypes = true;
        }

        private void ChkGrid_UnChecked(Base sender, EventArgs arguments)
        {
            editGrid = false;
        }

        private void ChkGrid_Checked(Base sender, EventArgs arguments)
        {
            editGrid = true;
        }

        private void ModeGroup_SelectionChanged(Base sender, ItemSelectedEventArgs arguments)
        {
            RadioButtonGroup mode = sender as RadioButtonGroup;
            LabeledRadioButton select = mode.Selected;

            if (select.Text == " Layer")
            {
                editMode = (int)EditorModes.Layer;
            }
            if (select.Text == " Type")
            {
                editMode = (int)EditorModes.Type;
            }
            if (select.Text == " Tileset")
            {
                editMode = (int)EditorModes.Tilesets;
            }
        }

        private void TypeGroup_SelectionChanged(Base sender, ItemSelectedEventArgs arguments)
        {
            RadioButtonGroup type = sender as RadioButtonGroup;
            LabeledRadioButton select = type.Selected;

            if (select.Text == " None")
            {
                editType = (int)TileType.None;
            }
            if (select.Text == " Blocked")
            {
                editType = (int)TileType.Blocked;
            }
            if (select.Text == " NPC Spawn")
            {
                CreateNpcSpawnSelectWindow(type.Parent);
                editType = (int)TileType.NPCSpawn;
            }
        }

        private void NpcNum_ValueChanged(Base sender, EventArgs arguments)
        {
            npcLabel.SetText("Spawn: " + npcNum.Value);
            editselectNpc = (int)npcNum.Value;
        }

        public void CreateNpcSpawnSelectWindow(Base parent)
        {
            npcSelect = new WindowControl(parent.GetCanvas());
            npcSelect.SetBounds(new Rectangle(550, 25, 75, 150));
            npcSelect.Title = "Select NPC";

            npcLabel = new Label(npcSelect);
            npcLabel.SetText("Spawn: 1");
            npcLabel.SetPosition(10, 45);

            npcNum = new HorizontalSlider(npcSelect);
            npcNum.SetBounds(new Rectangle(10, 10, 65, 25));
            npcNum.SetRange(0, 10);
            npcNum.NotchCount = 10;
            npcNum.Value = 1;
            npcNum.SnapToNotches = true;
            npcNum.ValueChanged += NpcNum_ValueChanged;
        }

        private void LayerGroup_SelectionChanged(Base sender, ItemSelectedEventArgs arguments)
        {
            RadioButtonGroup layer = sender as RadioButtonGroup;
            LabeledRadioButton selected = layer.Selected;

            if (selected.Text == " Ground")
            {
                editLayer = (int)TileLayers.Ground;
            }
            if (selected.Text == " Mask")
            {
                editLayer = (int)TileLayers.Mask;
            }
            if (selected.Text == " MaskA")
            {
                editLayer = (int)TileLayers.MaskA;
            }
            if (selected.Text == " Fringe")
            {
                editLayer = (int)TileLayers.Fringe;
            }
            if (selected.Text == " FringeA")
            {
                editLayer = (int)TileLayers.FringeA;
            }
        }

        private void ClickChangeModeButton(Base control, ClickedEventArgs e)
        {
            if (modeWin == null || modeWin.IsVisible == false)
            {
                CreateModeWindow(control.GetCanvas());
            }
        }

        private void ClickLayerButton(Base control, ClickedEventArgs e)
        {
            if (layerWin == null || layerWin.IsVisible == false)
            {
                CreateLayerWindow(control.GetCanvas());
                CreatePaintWindow(control.GetCanvas());
            }
        }

        private void ClickNextTilesetButton(Base control, ClickedEventArgs e)
        {
            if (editTileset < 66)
            {
                editTileset += 1;
            }
            else
            {
                editTileset = 0;
            }
        }

        private void ClickPrevTilesetButton(Base control, ClickedEventArgs e)
        {
            if (editTileset > 0)
            {
                editTileset -= 1;
            }
            else
            {
                editTileset = 66;
            }
        }

        private void ClickTypeButton(Base control, ClickedEventArgs e)
        {
            if (typeWin == null || typeWin.IsVisible == false)
            {
                CreateTypeWindow(control.GetCanvas());
            }
        }
    }
}
