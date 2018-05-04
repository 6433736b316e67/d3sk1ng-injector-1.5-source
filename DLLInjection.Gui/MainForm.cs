namespace DLLInjection.Gui
{
    using DLLInjection;
    using DLLInjection.Gui.Properties;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Security.Principal;
    using System.Threading;
    using System.Windows.Forms;

    public class MainForm : Form
    {
        private bool hasAdminPerms;
        private IContainer components;
        private OpenFileDialog openFileDialog;
        private Button injectBtn;
        private Label InjectionStatusLabel;
        private PictureBox pictureBox1;
        private Label status_label;
        private PictureBox pictureBox2;

        public MainForm()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(MainForm));
            this.openFileDialog = new OpenFileDialog();
            this.injectBtn = new Button();
            this.InjectionStatusLabel = new Label();
            this.status_label = new Label();
            this.pictureBox2 = new PictureBox();
            this.pictureBox1 = new PictureBox();
            ((ISupportInitialize) this.pictureBox2).BeginInit();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.openFileDialog.Filter = "DLL Files (*.dll)|*.dll|All files (*.*)|*.*";
            this.openFileDialog.SupportMultiDottedExtensions = true;
            this.openFileDialog.Title = "Select DLL to inject...";
            this.injectBtn.FlatStyle = FlatStyle.Flat;
            this.injectBtn.ForeColor = Color.DarkGreen;
            this.injectBtn.Location = new Point(0x63, 0x8f);
            this.injectBtn.Name = "injectBtn";
            this.injectBtn.Size = new Size(0xc7, 0x2d);
            this.injectBtn.TabIndex = 6;
            this.injectBtn.Text = "INJECT";
            this.injectBtn.UseVisualStyleBackColor = true;
            this.injectBtn.Click += new EventHandler(this.injectBtn_Click);
            this.InjectionStatusLabel.AutoSize = true;
            this.InjectionStatusLabel.Location = new Point(0xdf, 0x119);
            this.InjectionStatusLabel.Name = "InjectionStatusLabel";
            this.InjectionStatusLabel.Size = new Size(0, 13);
            this.InjectionStatusLabel.TabIndex = 7;
            this.status_label.Anchor = AnchorStyles.Top;
            this.status_label.ForeColor = Color.ForestGreen;
            this.status_label.Location = new Point(0x60, 0x7a);
            this.status_label.Name = "status_label";
            this.status_label.Size = new Size(0xca, 13);
            this.status_label.TabIndex = 9;
            this.status_label.TextAlign = ContentAlignment.TopCenter;
            this.pictureBox2.Image = Resources.YouTube_icon1;
            this.pictureBox2.Location = new Point(0xb1, 190);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Size(0x2a, 0x24);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new EventHandler(this.pictureBox2_Click);
            this.pictureBox1.Image = Resources.d3sk_logo;
            this.pictureBox1.Location = new Point(0x63, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0xc7, 0x79);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(30, 30, 30);
            base.ClientSize = new Size(0x18c, 0xe2);
            base.Controls.Add(this.pictureBox2);
            base.Controls.Add(this.status_label);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.InjectionStatusLabel);
            base.Controls.Add(this.injectBtn);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "MainForm";
            this.Text = "D3SK1NG INJECTOR 1.5";
            base.Load += new EventHandler(this.MainForm_Load);
            ((ISupportInitialize) this.pictureBox2).EndInit();
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void injectBtn_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(this.InjectDLL)) { IsBackground = true }.Start();
        }

        public void InjectDLL()
        {
            Process process;
            string str;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                this.hasAdminPerms = new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
            }
            try
            {
                process = Process.GetProcessesByName("GTA5")[0];
                str = process.Id.ToString();
            }
            catch (IndexOutOfRangeException)
            {
                try
                {
                    process = Process.GetProcessesByName("FiveM")[0];
                    str = process.Id.ToString();
                }
                catch (IndexOutOfRangeException)
                {
                    this.status_label.Invoke(() => this.status_label.Text = "");
                    MessageBox.Show("GTA 5 IS NOT RUNNING!", "ERROR");
                    return;
                }
            }
            try
            {
                string str4;
                int num2;
                this.status_label.Invoke(() => this.status_label.Text = "INITIALIZING");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\D3SK1NG");
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\D3SK1NG\d3sk1ng.dll";
                string iniPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\D3SK1NG\settings.ini";
                using (WebClient client = new WebClient())
                {
                    str4 = client.DownloadString("https://d3sk1ng.com/update/last.php");
                }
                IniFile file = new IniFile(iniPath);
                if (file.KeyExists("CURRENT_MENU_VERSION", "INJECTOR") && System.IO.File.Exists(path))
                {
                    if (file.Read("CURRENT_MENU_VERSION", "INJECTOR").ToString() != str4)
                    {
                        MessageBox.Show("An update is available!", "D3SK1NG");
                        Process.Start("https://d3sk1ng.com/update/changelog.php");
                        this.status_label.Invoke(() => this.status_label.Text = "DOWNLOADING");
                        using (WebClient client2 = new WebClient())
                        {
                            client2.DownloadFile("https://d3sk1ng.com/update/" + str4, path);
                        }
                        file.Write("CURRENT_MENU_VERSION", str4, "INJECTOR");
                    }
                }
                else
                {
                    this.status_label.Invoke(() => this.status_label.Text = "DOWNLOADING");
                    using (WebClient client3 = new WebClient())
                    {
                        client3.DownloadFile("https://d3sk1ng.com/update/" + str4, path);
                    }
                    file.Write("CURRENT_MENU_VERSION", str4, "INJECTOR");
                }
                this.status_label.Invoke(() => this.status_label.Text = "INJECTING");
                if (!int.TryParse(str, out num2))
                {
                    MessageBox.Show("Missing parameters!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (!System.IO.File.Exists(path))
                {
                    file.Write("CURRENT_MENU_VERSION", "-1", "INJECTOR");
                    MessageBox.Show("Cannot find the d3sk1ng dll!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    InjectionMethod injectionMethod = InjectionMethod.CREATE_REMOTE_THREAD;
                    try
                    {
                        new DLLInjector(injectionMethod).Inject(num2, path, null);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, exception.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    this.status_label.Invoke(() => this.status_label.Text = "D3SK1NG INJECTED SUCCESSFULLY!");
                    Thread.Sleep(0x7d0);
                    Application.Exit();
                }
            }
            catch (WebException)
            {
                process.Kill();
                MessageBox.Show("The menu file has been updated. Please restart the game and press inject again!");
                this.status_label.Invoke(() => this.status_label.Text = "");
            }
        }

        public static bool IsFileReadOnly(string FileName) => 
            new FileInfo(FileName).IsReadOnly;

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + (Environment.Is64BitProcess ? " (amd64)" : " (x86)");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("https://d3sk1ng.com/youtube");
        }
    }
}

