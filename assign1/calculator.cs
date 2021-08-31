using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;


public class CalculatorUI : Form
{
	private Label title = new Label();
	private Label author = new Label();
	private Label radius_prompt = new Label();
	private TextBox radius_input_area = new TextBox();
	private Label circ_output_area = new Label();
	private Label area_output_area = new Label();

	private Button compute_button = new Button();
	private Button clear_button = new Button();
	private Button exit_button = new Button();

	private Panel header_panel = new Panel();
	private Panel display_panel = new Panel();
	private Panel control_panel = new Panel();

	private Size max_win_size = new Size(1024,800);
	private Size min_win_size = new Size(1024,800);


	private enum Execution_state {Executing, Waiting_to_terminate};
	private static System.Timers.Timer exit_clock = new System.Timers.Timer();
	private Execution_state current_state = Execution_state.Executing;


	// Constructor
	public CalculatorUI()
	{
		MaximumSize = max_win_size;
		MinimumSize = min_win_size;

		// Set strings
		Text = "Circle Calculator";
		title.Text = "Circle Calculator";
		author.Text = "By Zachary Thompson";
		radius_prompt.Text = "Enter a radius:";
		circ_output_area.Text = "The circumference will be displayed here.";
		area_output_area.Text = "The surface area will be displayed here.";
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
		radius_prompt.Size = new Size(300,36);
		radius_input_area.Size = new Size(400,30);
		circ_output_area.Size = new Size(800,50);
		area_output_area.Size = new Size(800,50);

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
		circ_output_area.ForeColor = Color.Black;
		area_output_area.ForeColor = Color.Black;

		// Set fonts
		title.Font = new Font("Arial",26,FontStyle.Regular);
		author.Font = new Font("Arial",20,FontStyle.Regular);
		radius_prompt.Font = new Font("Arial",26,FontStyle.Regular);
		radius_input_area.Font = new Font("Arial",26,FontStyle.Regular);
		circ_output_area.Font = new Font("Arial",26,FontStyle.Regular);
		area_output_area.Font = new Font("Arial",26,FontStyle.Regular);
		compute_button.Font = new Font("Arial",18,FontStyle.Regular);
		clear_button.Font = new Font("Arial",18,FontStyle.Regular);
		exit_button.Font = new Font("Arial",18,FontStyle.Regular);

		// Set locations
		header_panel.Location = new Point(0,0);
		display_panel.Location = new Point(0,200);
		control_panel.Location = new Point(0,600);
		title.Location = new Point(375,26);
		author.Location = new Point(330,100);
		radius_prompt.Location = new Point(50,60);
		radius_input_area.Location = new Point(450,60);
		circ_output_area.Location = new Point(50,175);
		area_output_area.Location = new Point(50,250);
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
		display_panel.Controls.Add(circ_output_area);
		display_panel.Controls.Add(area_output_area);
		Controls.Add(control_panel);
		control_panel.Controls.Add(compute_button);
		control_panel.Controls.Add(clear_button);
		control_panel.Controls.Add(exit_button);

		CenterToScreen();

		// Setup event handlers
		compute_button.Click += new EventHandler(circleCalculate);
		clear_button.Click += new EventHandler(clear);
		exit_button.Click += new EventHandler(stoprun);

		
		exit_clock.Enabled = false; //Clock is turned off at start program execution.
		exit_clock.Interval = 3500; //3500ms = 3.5seconds
		exit_clock.Elapsed += new ElapsedEventHandler(shutdown);   //Attach a method to the clock.
	}

	protected void circleCalculate(Object sender, EventArgs events)
	{
		double radius;
		string circ_output;
		string area_output;

		try
		{
			radius = double.Parse(radius_input_area.Text);
			if (radius < 0)
			{
				circ_output = "Invalid input. Please try again.";
				area_output = "";
			}
			else
			{
				double circ = 2 * Math.PI * radius;
				double area = Math.PI * radius * radius;
				circ_output = "The circumference is: " + Math.Round(circ, 6) + " units";
				area_output = "The interior area is: " + Math.Round(area, 6) + " units";
			}
		}
		catch
		{
			circ_output = "Invalid input. Please try again.";
			area_output = "";
		}

		circ_output_area.Text = circ_output;
		area_output_area.Text = area_output;
		display_panel.Invalidate();
	}
	
	
	protected void stoprun(Object sender, EventArgs events)
	{
		switch(current_state)
		{
			case Execution_state.Executing:
				exit_clock.Interval= 3500;
				exit_clock.Enabled = true;
				exit_button.Text = "Resume Execution";
				current_state = Execution_state.Waiting_to_terminate;
				break;
			case Execution_state.Waiting_to_terminate:
				exit_clock.Enabled = false;
				exit_button.Text = "Exit";
				current_state = Execution_state.Executing;
				break;
		}
	}

	protected void shutdown(System.Object sender, EventArgs even)
	{
		Close();
	}

	protected void clear(Object sender, EventArgs events)
	{
		radius_input_area.Text = "";
		circ_output_area.Text = "The circumference will be displayed here.";
		area_output_area.Text = "The surface area will be displayed here.";
	}
}
