using System;
using System.Drawing;
using System.Windows.Forms;

public class CalculatorUI : Form
{
	private Label title = new Label();
	private Label author = new Label();
	private Label radius_prompt = new Label();
	private TextBox radius_input_area = new TextBox();

	private Button compute_button = new Button();
	private Button clear_button = new Button();
	private Button exit_button = new Button();

	private Panel header_panel = new Panel();
	private Panel display_panel = new Panel();
	private Panel control_panel = new Panel();

	private Size max_win_size = new Size(1024,800);
	private Size min_win_size = new Size(1024,800);


	// Constructor
	public CalculatorUI()
	{



		//title.TextAlign = ContentAlignment.MiddleCenter;
		title.BackColor = Color.White;
		MaximumSize = max_win_size;
		MinimumSize = min_win_size;

		// Set strings
		Text = "Circle Calculator";
		title.Text = "Circle Calculator";
		author.Text = "By Zachary Thompson";
		radius_prompt.Text = "Enter a radius:";
		compute_button.Text = "Compute";
		clear_button.Text = "Clear";
		exit_button.Text = "Exit";

		// Set sizes
		Size = new Size(400,240);
		header_panel.Size = new Size(1024,200);
		display_panel.Size = new Size(1024,400);
		control_panel.Size = new Size(1024,200);
		compute_button.Size = new Size(120,60);
		clear_button.Size = new Size(120,60);
		exit_button.Size = new Size(120,60);
		title.Size = new Size(800,44);
		author.Size = new Size(320,34);
		radius_prompt.Size = new Size(400,36);
		radius_input_area.Size = new Size(200,30);

		// Set colors
		header_panel.BackColor = Color.Yellow;
		display_panel.BackColor = Color.LightGray;
		control_panel.BackColor = Color.LightGreen;
		compute_button.BackColor = Color.Orange;
		clear_button.BackColor = Color.Aquamarine;
		exit_button.BackColor = Color.Red;
		title.ForeColor = Color.Black;
		author.ForeColor = Color.Black;
		radius_prompt.ForeColor = Color.Black;
		compute_button.ForeColor = Color.Black;
		clear_button.ForeColor = Color.Black;
		exit_button.ForeColor = Color.Black;

		// Set fonts
		title.Font = new Font("Arial",26,FontStyle.Regular);
		author.Font = new Font("Arial",20,FontStyle.Regular);
		radius_prompt.Font = new Font("Arial",26,FontStyle.Regular);
		radius_input_area.Font = new Font("Arial",26,FontStyle.Regular);
		compute_button.Font = new Font("Arial",26,FontStyle.Regular);
		clear_button.Font = new Font("Arial",26,FontStyle.Regular);
		exit_button.Font = new Font("Arial",26,FontStyle.Regular);

		// Set locations
		header_panel.Location = new Point(0,0);
		display_panel.Location = new Point(0,200);
		control_panel.Location = new Point(0,600);
		title.Location = new Point(375,26);
		author.Location = new Point(330,100);
		radius_prompt.Location = new Point(100,60);
		radius_input_area.Location = new Point(600,60);
		compute_button.Location = new Point(200,50);
		clear_button.Location = new Point(450,50);
		exit_button.Location = new Point(720,50);

		AcceptButton = compute_button;

		// Add controls to the form
		Controls.Add(header_panel);
		header_panel.Controls.Add(title);
		header_panel.Controls.Add(author);
		Controls.Add(display_panel);
		display_panel.Controls.Add(radius_prompt);
		display_panel.Controls.Add(radius_input_area);
		Controls.Add(control_panel);
		control_panel.Controls.Add(compute_button);
		control_panel.Controls.Add(clear_button);
		control_panel.Controls.Add(exit_button);



		CenterToScreen();
	}
}
