using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class AppTheme
    {
        // Main application colours
        public static Color Primary = Color.FromArgb(31, 45, 61);
        public static Color Accent = Color.FromArgb(52, 152, 219);

        // Status/action colours
        public static Color Success = Color.FromArgb(39, 174, 96);
        public static Color Warning = Color.FromArgb(243, 156, 18);
        public static Color Danger = Color.FromArgb(192, 57, 43);

        // Background colours
        public static Color Background = Color.FromArgb(245, 247, 250);
        public static Color White = Color.White;

        // Text colours
        public static Color Text = Color.FromArgb(44, 62, 80);
        public static Color MutedText = Color.FromArgb(108, 117, 125);


        // Apply the general theme to a form
        public static void ApplyToForm(Form form)
        {
            form.BackColor = Background;
            form.Font = new Font("Segoe UI", 9F);

            StyleControls(form.Controls);
            AddBoreholeWatermark(form);
        }


        // Recursively style controls, including controls inside panels/group boxes
        private static void StyleControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Button button)
                {
                    StyleButton(button);
                }
                else if (control is Label label)
                {
                    label.ForeColor = Text;
                    label.Font = new Font("Segoe UI", 9F);
                }
                else if (control is TextBox textBox)
                {
                    textBox.Font = new Font("Segoe UI", 9F);
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.Font = new Font("Segoe UI", 9F);
                }
                else if (control is DataGridView grid)
                {
                    StyleDataGridView(grid);
                }

                // Continue into panels, group boxes, etc.
                if (control.HasChildren)
                {
                    StyleControls(control.Controls);
                }
            }
        }


        // Button styling + engineering-related icons
        private static void StyleButton(Button button)
        {
            string text = button.Text.Trim();

            // Remove existing icons so they are not duplicated
            text = text
                .Replace("📋", "")
                .Replace("🔍", "")
                .Replace("💾", "")
                .Replace("📤", "")
                .Replace("✕", "")
                .Replace("✓", "")
                .Replace("🧹", "")
                .Replace("↩️", "")
                .Replace("💧", "")
                .Replace("🛠️", "")
                .Replace("👤", "")
                .Replace("👨‍💼", "")
                .Replace("🏢", "")
                .Replace("⚙️", "")
                .Replace("＋", "")
                .Replace("✎", "")
                .Replace("🗑️", "")
                .Trim();

            string lowerText = text.ToLower();

            if (lowerText.Contains("submit"))
            {
                button.Text = "📤  " + text;
                button.BackColor = Accent;
            }
            else if (lowerText.Contains("save"))
            {
                button.Text = "💾  " + text;
                button.BackColor = Accent;
            }
            else if (lowerText.Contains("cancel"))
            {
                button.Text = "✕  " + text;
                button.BackColor = Danger;
            }
            else if (lowerText.Contains("clear"))
            {
                button.Text = "🧹  " + text;
                button.BackColor = Primary;
            }
            else if (lowerText.Contains("view"))
            {
                button.Text = "🔍  " + text;
                button.BackColor = Accent;
            }
            else if (lowerText.Contains("approve") ||
                     lowerText.Contains("complete"))
            {
                button.Text = "✓  " + text;
                button.BackColor = Success;
            }
            else if (lowerText.Contains("technician"))
            {
                button.Text = "🛠️  " + text;
                button.BackColor = Primary;
            }
            else if (lowerText.Contains("clerk"))
            {
                button.Text = "👨‍💼  " + text;
                button.BackColor = Primary;
            }
            else if (lowerText.Contains("client"))
            {
                button.Text = "👤  " + text;
                button.BackColor = Primary;
            }
            else if (lowerText.Contains("provider") ||
                     lowerText.Contains("dsp") ||
                     lowerText.Contains("organisation") ||
                     lowerText.Contains("organization"))
            {
                button.Text = "🏢  " + text;
                button.BackColor = Primary;
            }
            else if (lowerText.Contains("service"))
            {
                button.Text = "⚙️  " + text;
                button.BackColor = Primary;
            }
            else if (lowerText.Contains("add") ||
                     lowerText.Contains("new"))
            {
                button.Text = "＋  " + text;
                button.BackColor = Accent;
            }
            else if (lowerText.Contains("edit") ||
                     lowerText.Contains("update"))
            {
                button.Text = "✎  " + text;
                button.BackColor = Accent;
            }
            else if (lowerText.Contains("delete") ||
                     lowerText.Contains("remove"))
            {
                button.Text = "🗑️  " + text;
                button.BackColor = Danger;
            }
            else if (lowerText.Contains("request") ||
                     lowerText.Contains("book"))
            {
                button.Text = "📋  " + text;
                button.BackColor = Accent;
            }
            else if (lowerText.Contains("water"))
            {
                button.Text = "💧  " + text;
                button.BackColor = Accent;
            }
            else if (lowerText.Contains("site"))
            {
                button.Text = "📍  " + text;
                button.BackColor = Accent;
            }
            else if (lowerText.Contains("back"))
            {
                button.Text = "↩️  " + text;
                button.BackColor = Primary;
            }
            else if (lowerText.Contains("logout") ||
                     lowerText.Contains("log out"))
            {
                button.Text = "↩️  " + text;
                button.BackColor = Danger;
            }
            else
            {
                button.Text = text;
                button.BackColor = Primary;
            }

            button.ForeColor = White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.TextAlign = ContentAlignment.MiddleCenter;
        }


        // DataGridView styling
        private static void StyleDataGridView(DataGridView grid)
        {
            grid.BackgroundColor = White;
            grid.BorderStyle = BorderStyle.None;

            // Header
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = White;
            grid.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            grid.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            grid.ColumnHeadersHeight = 38;

            // Rows
            grid.DefaultCellStyle.Font =
                new Font("Segoe UI", 9F);

            grid.DefaultCellStyle.ForeColor = Text;
            grid.DefaultCellStyle.BackColor = White;

            grid.DefaultCellStyle.SelectionBackColor = Accent;
            grid.DefaultCellStyle.SelectionForeColor = White;

            grid.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 247, 250);

            grid.RowTemplate.Height = 34;

            // Remove row header area
            grid.RowHeadersVisible = false;

            // Select an entire record
            grid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            grid.MultiSelect = false;

            // Cleaner appearance
            grid.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            grid.GridColor =
                Color.FromArgb(225, 229, 234);

            grid.AllowUserToAddRows = false;
            grid.AllowUserToResizeRows = false;

        }
        public static void ApplyAdminDashboardStyle(Form form)
        {
            // Main form
            form.BackColor = Background;
            form.Font = new Font("Segoe UI", 9F);

            StyleAdminControls(form.Controls);
        }
        private static void StyleAdminControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Button button)
                {
                    StyleAdminButton(button);
                }
                else if (control is Label label)
                {
                    label.ForeColor = Text;

                    if (label.Text.ToUpper().Contains("BOREHOLE") ||
                        label.Text.ToUpper().Contains("ADMIN"))
                    {
                        label.Font = new Font(
                            "Segoe UI",
                            16F,
                            FontStyle.Bold
                        );

                        label.ForeColor = Primary;
                    }
                    else
                    {
                        label.Font = new Font(
                            "Segoe UI",
                            9F
                        );
                    }
                }
                else if (control is DataGridView grid)
                {
                    StyleDataGridView(grid);
                }

                // Also style controls inside panels/group boxes
                if (control.HasChildren)
                {
                    StyleAdminControls(control.Controls);
                }
            }
        }
        private static void StyleAdminButton(Button button)
        {
            string originalText = button.Text.Trim();

            // Remove existing icons to prevent duplicates
            string text = originalText
                .Replace("👤", "")
                .Replace("👨‍💼", "")
                .Replace("🛠️", "")
                .Replace("🏢", "")
                .Replace("⚙️", "")
                .Replace("＋", "")
                .Replace("✎", "")
                .Replace("🗑️", "")
                .Replace("📋", "")
                .Replace("↩️", "")
                .Replace("🔍", "")
                .Trim();

            string lowerText = text.ToLower();

            // Clients
            if (lowerText.Contains("client"))
            {
                button.Text = "👤  " + text;
                button.BackColor = Accent;
            }

            // Clerks
            else if (lowerText.Contains("clerk"))
            {
                button.Text = "👨‍💼  " + text;
                button.BackColor = Primary;
            }

            // Technicians
            else if (lowerText.Contains("technician"))
            {
                button.Text = "🛠️  " + text;
                button.BackColor = Primary;
            }

            // DSP / Organisation
            else if (lowerText.Contains("dsp") ||
                     lowerText.Contains("provider") ||
                     lowerText.Contains("organisation") ||
                     lowerText.Contains("organization"))
            {
                button.Text = "🏢  " + text;
                button.BackColor = Primary;
            }

            // Services
            else if (lowerText.Contains("service"))
            {
                button.Text = "⚙️  " + text;
                button.BackColor = Primary;
            }

            // Add/New
            else if (lowerText.Contains("add") ||
                     lowerText.Contains("new"))
            {
                button.Text = "＋  " + text;
                button.BackColor = Accent;
            }

            // Edit/Update
            else if (lowerText.Contains("edit") ||
                     lowerText.Contains("update"))
            {
                button.Text = "✎  " + text;
                button.BackColor = Accent;
            }

            // Delete/Remove
            else if (lowerText.Contains("delete") ||
                     lowerText.Contains("remove"))
            {
                button.Text = "🗑️  " + text;
                button.BackColor = Danger;
            }

            // Back
            else if (lowerText.Contains("back"))
            {
                button.Text = "↩️  " + text;
                button.BackColor = Primary;
            }

            // Logout
            else if (lowerText.Contains("logout") ||
                     lowerText.Contains("log out"))
            {
                button.Text = "↩️  " + text;
                button.BackColor = Danger;
            }

            // Default admin button
            else
            {
                button.Text = text;
                button.BackColor = Primary;
            }


            button.ForeColor = White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold
            );

            button.Cursor = Cursors.Hand;
            button.TextAlign = ContentAlignment.MiddleCenter;
        }

        private static void AddBoreholeWatermark(Form form)
        {
            // Prevent the event from being added more than once
            form.Paint -= DrawBoreholeWatermark;
            form.Paint += DrawBoreholeWatermark;
        }

        private static void DrawBoreholeWatermark(object sender, PaintEventArgs e)
        {
            Form form = sender as Form;

            if (form == null)
                return;

            Graphics g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Scale watermark according to form size
            float scale = Math.Min(form.ClientSize.Width, form.ClientSize.Height) / 700f;

            if (scale < 0.65f)
                scale = 0.65f;

            if (scale > 1.2f)
                scale = 1.2f;

            // Position: lower-right area
            float centerX = form.ClientSize.Width - (180 * scale);
            float baseY = form.ClientSize.Height - (80 * scale);

            using (Pen watermarkPen =
                   new Pen(Color.FromArgb(22, 52, 152, 219), 4 * scale))
            using (Pen lightPen =
                   new Pen(Color.FromArgb(16, 52, 152, 219), 2 * scale))
            using (Brush watermarkBrush =
                   new SolidBrush(Color.FromArgb(15, 52, 152, 219)))
            using (Font watermarkFont =
                   new Font("Segoe UI", 20 * scale, FontStyle.Bold))
            {
                // -----------------------------
                // DRILLING RIG / DERRICK
                // -----------------------------

                float rigTopY = baseY - (260 * scale);
                float rigBottomY = baseY - (30 * scale);

                // Left leg
                g.DrawLine(
                    watermarkPen,
                    centerX - (65 * scale), rigBottomY,
                    centerX, rigTopY);

                // Right leg
                g.DrawLine(
                    watermarkPen,
                    centerX + (65 * scale), rigBottomY,
                    centerX, rigTopY);

                // Derrick cross beams
                g.DrawLine(
                    lightPen,
                    centerX - (48 * scale), rigBottomY - (65 * scale),
                    centerX + (48 * scale), rigBottomY - (65 * scale));

                g.DrawLine(
                    lightPen,
                    centerX - (32 * scale), rigBottomY - (125 * scale),
                    centerX + (32 * scale), rigBottomY - (125 * scale));

                // Top of rig
                g.DrawLine(
                    watermarkPen,
                    centerX - (18 * scale), rigTopY + (30 * scale),
                    centerX + (18 * scale), rigTopY + (30 * scale));

                // -----------------------------
                // DRILL PIPE
                // -----------------------------

                float pipeTop = rigTopY + (30 * scale);
                float pipeBottom = baseY + (35 * scale);

                g.DrawLine(
                    watermarkPen,
                    centerX, pipeTop,
                    centerX, pipeBottom);

                // -----------------------------
                // DRILL BIT
                // -----------------------------

                PointF[] drillBit =
                {
            new PointF(centerX - (20 * scale), pipeBottom),
            new PointF(centerX + (20 * scale), pipeBottom),
            new PointF(centerX, pipeBottom + (35 * scale))
        };

                g.DrawPolygon(watermarkPen, drillBit);

                // -----------------------------
                // BOREHOLE / GROUND
                // -----------------------------

                g.DrawLine(
                    watermarkPen,
                    centerX - (100 * scale), baseY + (35 * scale),
                    centerX + (100 * scale), baseY + (35 * scale));

                // Ground layers
                g.DrawEllipse(
                    lightPen,
                    centerX - (55 * scale),
                    baseY + (50 * scale),
                    110 * scale,
                    28 * scale);

                g.DrawEllipse(
                    lightPen,
                    centerX - (70 * scale),
                    baseY + (62 * scale),
                    150 * scale,
                    35 * scale);

                // -----------------------------
                // WATER / BOREHOLE WAVES
                // -----------------------------

                g.DrawArc(
                    lightPen,
                    centerX - (95 * scale),
                    baseY + (85 * scale),
                    190 * scale,
                    55 * scale,
                    0,
                    180);

                g.DrawArc(
                    lightPen,
                    centerX - (75 * scale),
                    baseY + (105 * scale),
                    150 * scale,
                    45 * scale,
                    0,
                    180);

                // -----------------------------
                // WATER DROP
                // -----------------------------

                PointF[] drop =
                {
            new PointF(
                centerX + (85 * scale),
                baseY - (25 * scale)),

            new PointF(
                centerX + (65 * scale),
                baseY + (5 * scale)),

            new PointF(
                centerX + (85 * scale),
                baseY + (25 * scale)),

            new PointF(
                centerX + (105 * scale),
                baseY + (5 * scale))
        };

                g.DrawPolygon(lightPen, drop);

                // -----------------------------
                // WATERMARK TEXT
                // -----------------------------

                string text = "BOREHOLE DRILLING";

                SizeF textSize = g.MeasureString(text, watermarkFont);

                g.DrawString(
                    text,
                    watermarkFont,
                    watermarkBrush,
                    centerX - (textSize.Width / 2),
                    baseY + (145 * scale));
            }
        }

    }
    }


