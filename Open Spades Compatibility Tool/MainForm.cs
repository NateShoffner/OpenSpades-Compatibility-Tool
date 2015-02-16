#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace OpenSpadesCompatibilityTool
{
    public partial class MainForm : Form
    {
        private const int OPENGL_REQUIRED_MAX_TEXTURE_SIZE = 1024;
        private const int OPENGL_REQUIRED_MAX_3D_TEXTURE_SIZE = 512;
        private const int OPENGL_REQUIRED_MAX_COMBINED_TEXTURE_IMAGE_UNITS = 12;
        private const int OPENGL_REQUIRED_MAX_VERTEX_TEXTURE_IMAGE_UNITS = 3;
        private const int OPENGL_REQUIRED_MAX_VARYING_COMPONENTS = 37;

        private const string SDL_DLL = "SDL.dll";
        private static readonly Size SDL_MODE_MINIMUM = new Size(800, 600);

        private readonly List<string> optionalOpenGLExtensions = new List<string>
        {
            "GL_ARB_framebuffer_sRGB",
            "GL_EXT_framebuffer_blit",
            "GL_EXT_texture_filter_anisotropic",
            "GL_ARB_occlusion_query",
            "GL_NV_conditional_render",
            "GL_ARB_color_buffer_float"
        };

        private readonly List<string> requiredOpenGLExtensions = new List<string>
        {
            "GL_ARB_multitexture",
            "GL_ARB_shader_objects",
            "GL_ARB_shading_language_100",
            "GL_ARB_texture_non_power_of_two",
            "GL_ARB_vertex_buffer_object",
            "GL_EXT_framebuffer_object",
        };

        public MainForm()
        {
            InitializeComponent();

            Text += string.Format(" {0}", Common.TruncateVersion(Application.ProductVersion));
            Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            checkbtn.PerformClick();
        }

        private void CheckCompatibility()
        {
            listExtensions.Items.Clear();
            listVariables.Items.Clear();
            listModes.Items.Clear();

            //opengl info
            using (var opengl = new OpenGL(Process.GetCurrentProcess().MainWindowHandle))
            {
                opengl.StartContext();

                lblVendor.Text = string.Format("Vendor: {0}", opengl.GetVendor());
                lblRenderer.Text = string.Format("Renderer: {0}", opengl.GetRenderer());
                lblVersion.Text = string.Format("Version: {0}", opengl.GetVersion());
                lblShader.Text = string.Format("GLSL: {0}", opengl.GetGLSLVersion());

                var availableExtensions = opengl.GetExtensions();

                var requiredCount = 0;

                foreach (var required in requiredOpenGLExtensions)
                {
                    var lvi = new ListViewItem(required);

                    if (availableExtensions.Contains(required))
                    {
                        lvi.SubItems.Add("Found");
                        lvi.SubItems[1].ForeColor = Color.Green;
                        requiredCount++;
                    }

                    else
                    {
                        lvi.SubItems.Add("Not Found");
                        lvi.SubItems[1].ForeColor = Color.Red;
                    }

                    listExtensions.Items.Add(lvi);
                    listExtensions.Items[listExtensions.Items.Count - 1].UseItemStyleForSubItems = false;
                }

                foreach (var optional in optionalOpenGLExtensions)
                {
                    var lvi = new ListViewItem(optional);

                    if (availableExtensions.Contains(optional))
                    {
                        lvi.SubItems.Add("Found");
                        lvi.SubItems[1].ForeColor = Color.Green;
                    }

                    else
                    {
                        lvi.SubItems.Add("Not Found/Not Required");
                        lvi.SubItems[1].ForeColor = Color.Orange;
                    }

                    listExtensions.Items.Add(lvi);
                    listExtensions.Items[listExtensions.Items.Count - 1].UseItemStyleForSubItems = false;
                }

                //variables
                var maxTextureSize = opengl.GetMaxTextureSize();
                var max3DTextureSize = opengl.GetMax3DTextureSize();
                var maxCombinedTextureImageUnits = opengl.GetMaxCombinedTextureUnits();
                var maxVertexTextureImageUnits = opengl.GetMaxVertexTextureImageUnits();
                var maxVaryingComponents = opengl.GetMaxVaryingComponents();

                var meetsMaxTextureSizeRequirements = maxTextureSize >= OPENGL_REQUIRED_MAX_TEXTURE_SIZE;
                var meetsMax3DTextureSizeRequirements = max3DTextureSize >= OPENGL_REQUIRED_MAX_3D_TEXTURE_SIZE;
                var meetsMaxCombinedTextureImageUnitsRequirements = maxCombinedTextureImageUnits >= OPENGL_REQUIRED_MAX_COMBINED_TEXTURE_IMAGE_UNITS;
                var meetsMaxVertexTextureImageUnitsRequirements = maxVertexTextureImageUnits >= OPENGL_REQUIRED_MAX_VERTEX_TEXTURE_IMAGE_UNITS;
                var meetsMaxVaryingComponentsRequirements = maxVaryingComponents >= OPENGL_REQUIRED_MAX_VARYING_COMPONENTS;

                var varItem = new ListViewItem(new[] {"Max Texture Size", maxTextureSize.ToString()}) {UseItemStyleForSubItems = false};
                varItem.SubItems[1].ForeColor = meetsMaxTextureSizeRequirements ? Color.Green : Color.Red;
                listVariables.Items.Add(varItem);

                varItem = new ListViewItem(new[] {"Max 3D Texture Size", max3DTextureSize.ToString()}) {UseItemStyleForSubItems = false};
                varItem.SubItems[1].ForeColor = meetsMax3DTextureSizeRequirements ? Color.Green : Color.Red;
                listVariables.Items.Add(varItem);

                varItem = new ListViewItem(new[] {"Max Combined Texture Image Units", maxCombinedTextureImageUnits.ToString()}) {UseItemStyleForSubItems = false};
                varItem.SubItems[1].ForeColor = meetsMaxCombinedTextureImageUnitsRequirements ? Color.Green : Color.Red;
                listVariables.Items.Add(varItem);

                varItem = new ListViewItem(new[] {"Max Vertex Texture Image Units", maxVertexTextureImageUnits.ToString()}) {UseItemStyleForSubItems = false};
                varItem.SubItems[1].ForeColor = meetsMaxVertexTextureImageUnitsRequirements ? Color.Green : Color.Red;
                listVariables.Items.Add(varItem);

                varItem = new ListViewItem(new[] {"Max Varying Components", maxVaryingComponents.ToString()}) {UseItemStyleForSubItems = false};
                varItem.SubItems[1].ForeColor = meetsMaxVaryingComponentsRequirements ? Color.Green : Color.Red;
                listVariables.Items.Add(varItem);

                var capable = requiredCount ==
                              requiredOpenGLExtensions.Count &&
                              meetsMaxTextureSizeRequirements &&
                              meetsMax3DTextureSizeRequirements &&
                              meetsMaxCombinedTextureImageUnitsRequirements &&
                              meetsMaxVertexTextureImageUnitsRequirements &&
                              meetsMaxVaryingComponentsRequirements;

                downloadbtn.Visible = capable;

                if (capable)
                {
                    lblresult.ForeColor = Color.Green;
                    lblresult.Text = "You are able to run OpenSpades!";
                }

                else
                {
                    lblresult.ForeColor = Color.Red;
                    lblresult.Text = "You are not able to run OpenSpades.";
                }

                lblresult.Visible = true;
            }

            if (File.Exists(SDL_DLL))
            {
                //SDL info
                var sdl = new SDL(SDL_DLL, SDL.SDL_INIT_VIDEO);

                foreach (var mode in sdl.ListModes(IntPtr.Zero, SDL.SDL_OPENGL | SDL.SDL_FULLSCREEN | SDL.SDL_DOUBLEBUF))
                {
                    if (mode.W >= SDL_MODE_MINIMUM.Width && mode.H >= SDL_MODE_MINIMUM.Height)
                    {
                        listModes.Items.Add(string.Format("{0}x{1}", mode.W, mode.H));
                    }
                }
            }

            else
            {
                listModes.Items.Add(string.Format("Unable to locate {0}", SDL_DLL));
            }
        }

        private void checkbtn_Click(object sender, EventArgs e)
        {
            try
            {
                CheckCompatibility();
            }

            catch (Exception ex)
            {
                File.AppendAllText("error.log", string.Format("{0}{1}{1}", ex.GetBaseException(), Environment.NewLine));
                MessageBox.Show("An error has occured. Please check the logs for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void downloadbtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/yvt/openspades/releases");
        }

        private void aboutbtn_Click(object sender, EventArgs e)
        {
            using (var ad = new AboutDialog())
            {
                ad.ShowDialog();
            }
        }
    }
}