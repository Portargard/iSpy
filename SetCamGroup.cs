using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace iSpyApplication
{
    public partial class SetCamGroup : Form
    {
        private bool setCamMode;
        public string nodeData;

        public SetCamGroup(bool setGroupForCam)
        {
            InitializeComponent();
            LoadData();
            setCamMode = setGroupForCam;
            if (setGroupForCam)
            {
                tbx_NewNode.Visible = false;
                lbl_NewNode.Visible = false;
                btn_RemoveNode.Visible = false;
                btn_confirm.Text = "OK";
                this.Text = "Set Camera Group";
            }
            else { this.Text = "Manager Camera Group"; }
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (setCamMode) 
            { 
                this.DialogResult = DialogResult.OK;
                this.Close();
                nodeData = trv_Data.SelectedNode.Text;
            }
            else
            {
                if (trv_Data.SelectedNode != null && tbx_NewNode.Text.Replace(" ","").Count()!=0)
                {
                    // Tạo node mới
                    TreeNode newNode = new TreeNode(tbx_NewNode.Text);

                    // Thêm node mới vào node đã chọn
                    trv_Data.SelectedNode.Nodes.Add(newNode);

                    // Mở rộng node cha để hiển thị node con
                    trv_Data.SelectedNode.Expand();
                    trv_Data.SelectedNode = null;
                    tbx_NewNode.Clear();
                }
                else
                {
                    if (tbx_NewNode.Text.Replace(" ", "").Count() != 0)
                    {
                        TreeNode newNode = new TreeNode(tbx_NewNode.Text);
                        trv_Data.Nodes.Add(newNode);
                    }
              }
            }
        }
        private void LoadData()
        {
            string filePath = Program.AppDataPath + @"XML/CamGroup.xml";
            var treeNodesData = LoadTreeViewFromXml(filePath);
            LoadDataToTreeView(treeNodesData);

        }
        private void SaveData()
        {
            string filePath = Program.AppDataPath + @"XML/CamGroup.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(TreeNodeData[]));

            TreeNodeData[] nodesData = GetTreeNodeData(trv_Data.Nodes);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, nodesData);
            }
        }

        private void btn_RemoveNode_Click(object sender, EventArgs e)
        {
            if (trv_Data.SelectedNode != null)
            {
                // Lưu lại node cha của node đã chọn
                TreeNode parentNode = trv_Data.SelectedNode.Parent;

                // Xoá node đã chọn
                trv_Data.Nodes.Remove(trv_Data.SelectedNode);

                // Hoặc nếu bạn muốn xoá node trong trường hợp nó có parent:
                //parentNode.Nodes.Remove(trv_Data.SelectedNode);
            }
        }

        #region function
        //save
        private TreeNodeData[] GetTreeNodeData(TreeNodeCollection nodes)
        {
            List<TreeNodeData> nodeDataList = new List<TreeNodeData>();

            foreach (TreeNode node in nodes)
            {
                TreeNodeData nodeData = new TreeNodeData
                {
                    Name = node.Text,
                    Children = GetTreeNodeData(node.Nodes).ToList()
                };
                nodeDataList.Add(nodeData);
            }

            return nodeDataList.ToArray();
        }
        //load
        private void LoadDataToTreeView(TreeNodeData[] treeNodesData)
        {
            trv_Data.Nodes.Clear(); // Xoá các node cũ trước khi thêm dữ liệu mới

            foreach (var nodeData in treeNodesData)
            {
                AddNodeToTreeView(nodeData, null);
            }
        }
        private void AddNodeToTreeView(TreeNodeData nodeData, TreeNode parentNode)
        {
            TreeNode newNode = new TreeNode(nodeData.Name);

            if (parentNode == null)
            {
                trv_Data.Nodes.Add(newNode);
            }
            else
            {
                parentNode.Nodes.Add(newNode);
            }

            foreach (var childNodeData in nodeData.Children)
            {
                AddNodeToTreeView(childNodeData, newNode);
            }
        }
        
        private TreeNodeData[] LoadTreeViewFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TreeNodeData[]));
            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    Console.Write("");
                }
            }
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    return (TreeNodeData[])serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                return (new List<TreeNodeData>()).ToArray();
            }
        }

        #endregion

        private void SetCamGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!setCamMode)
            {
                SaveData();
            }
        }
    }
}
[Serializable]
public class TreeNodeData
{
    public string Name { get; set; }
    public List<TreeNodeData> Children { get; set; } = new List<TreeNodeData>();
}
