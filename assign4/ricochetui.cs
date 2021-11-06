using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class RicochetUI : Form
{
	private Panel header_panel = new Panel();
	private Graphic_Panel display_panel = new Graphic_Panel();
	private Panel control_panel = new Panel();

	private Label title = new Label();
	private Label coordinates_label = new Label();
	private Label x_label = new Label();
	private Label y_label = new Label();
	private Label direction_label = new Label();
	private Label speed_label = new Label();

	private TextBox x_output = new TextBox();
	private TextBox y_output = new TextBox();
	private TextBox direction_input = new TextBox();
	private TextBox speed_input = new TextBox();

	private Button start_button = new Button();
	private Button reset_button = new Button();
	private Button exit_button = new Button();

	private Size max_win_size = new Size(800,900);
	private Size min_win_size = new Size(800,900);

	static private bool paused;

	static private double ball_radius = 20;
	private double ball_speed;
	private double ball_direction;
	static private double ball_pos_x;
	static private double ball_pos_y;

	private System.Timers.Timer animation_timer = new System.Timers.Timer();
	
	public RicochetUI()
	{
		// Window Sizes
		MaximumSize = max_win_size;
		MinimumSize = min_win_size;

		// Text
		Text = "Ricochet Ball";
		title.Text = "Ricochet Ball by Zachary Thompson";
		start_button.Text = "Start";
		reset_button.Text = "Reset";
		exit_button.Text = "Exit";
		coordinates_label.Text = "Coordinates of center of ball";
		x_label.Text = "X = ";
		y_label.Text = "Y = ";
		direction_label.Text = "Enter Direction (degrees)";
		speed_label.Text = "Enter Speed (pixel/second)";

		// Sizes
		Size = MinimumSize;
		header_panel.Size = new Size(800,100);
		display_panel.Size = new Size(800,600);
		control_panel.Size = new Size(800,200);
		start_button.Size = new Size(75,50);
		reset_button.Size = new Size(75,50);
		exit_button.Size = new Size(75,50);
		title.Size = new Size(500, 50);
		x_label.Size = new Size(25,25);
		y_label.Size = new Size(25,25);
		direction_label.Size = new Size(75,25);
		speed_label.Size = new Size(75,25);
		x_output.Size = new Size(50,50);
		y_output.Size = new Size(50,50);
		direction_input.Size = new Size(50,50);
		speed_input.Size = new Size(50,50);

		// Colors
		header_panel.BackColor = Color.LightBlue;
		display_panel.BackColor = Color.Green;
		control_panel.BackColor = Color.Yellow;
		start_button.BackColor = Color.White;
		reset_button.BackColor = Color.White;
		exit_button.BackColor = Color.White;
		start_button.ForeColor = Color.Black;
		reset_button.ForeColor = Color.Black;
		exit_button.ForeColor = Color.Black;
		x_label.ForeColor = Color.Black;
		y_label.ForeColor = Color.Black;
		direction_label.ForeColor = Color.Black;
		speed_label.ForeColor = Color.Black;
		x_output.ForeColor = Color.Black;
		y_output.ForeColor = Color.Black;
		direction_input.ForeColor = Color.Black;
		speed_input.ForeColor = Color.Black;
		x_output.BackColor = Color.White;
		y_output.BackColor = Color.White;
		direction_input.BackColor = Color.White;
		speed_input.BackColor = Color.White;

		// Fonts
		title.Font = new Font("Arial",18,FontStyle.Regular);
		start_button.Font = new Font("Arial",13,FontStyle.Regular);
		reset_button.Font = new Font("Arial",13,FontStyle.Regular);
		exit_button.Font = new Font("Arial",13,FontStyle.Regular);
		x_label.Font = new Font("Arial",13,FontStyle.Regular);
		y_label.Font = new Font("Arial",13,FontStyle.Regular);
		direction_label.Font = new Font("Arial",13,FontStyle.Regular);
		speed_label.Font = new Font("Arial",13,FontStyle.Regular);
		x_output.Font = new Font("Arial",13,FontStyle.Regular);
		y_output.Font = new Font("Arial",13,FontStyle.Regular);
		direction_input.Font = new Font("Arial",13,FontStyle.Regular);
		speed_input.Font = new Font("Arial",13,FontStyle.Regular);

		// Locations
		header_panel.Location = new Point(0,0);
		display_panel.Location = new Point(0,100);
		control_panel.Location = new Point(0,700);
		title.Location = new Point(250,35);
		start_button.Location = new Point(50,100);
		reset_button.Location = new Point(50,30);
		exit_button.Location = new Point(710,100);

		// Enter key presses start
		AcceptButton = start_button;

		// Add Controls
		Controls.Add(header_panel);
		header_panel.Controls.Add(title);
		Controls.Add(display_panel);
		Controls.Add(control_panel);
		control_panel.Controls.Add(start_button);
		control_panel.Controls.Add(reset_button);
		control_panel.Controls.Add(exit_button);

		// Event Handlers
		start_button.Click += new EventHandler(Start);
		reset_button.Click += new EventHandler(Reset);
		exit_button.Click += new EventHandler(Exit);

		// Setup Timer
		animation_timer.Enabled = false;
		animation_timer.Interval = (int)Math.Round(1000.0/30.0); // 34 approx. equals 30 hz
		animation_timer.Elapsed += new ElapsedEventHandler(MoveBall);

		// Initial Values
		paused = true;
		ball_speed = 5.0;
		ball_direction = 150.0;
		ball_pos_x = 400;
		ball_pos_y = 300;

		display_panel.Invalidate();
	}

	private double DegToRad(double angle)
	{
		return angle * (Math.PI/180);
	}

	protected void MoveBall(Object sender, ElapsedEventArgs events)
	{
		ball_pos_x = ball_pos_x + ball_speed * Math.Cos(DegToRad(ball_direction));
		ball_pos_y = ball_pos_y + ball_speed * Math.Sin(DegToRad(ball_direction));
		DetectCollision();
		Console.WriteLine($"Direction: {ball_direction}");
		display_panel.Invalidate();
	}


	private void DetectCollision()
	{
		// Left
		if (ball_pos_x < 0)
		{
			ball_direction = 180-ball_direction;
			ball_pos_x = 0;
		}
		// Right
		if (ball_pos_x + 2*ball_radius > 800)
		{
			ball_direction = 180-ball_direction;
			ball_pos_x = 800 - 2*ball_radius;
		}
		// Up
		if (ball_pos_y < 0)
		{
			ball_direction = -ball_direction;
			ball_pos_y = 0;
		}
		// Down
		if (ball_pos_y + 2*ball_radius > 600)
		{
			ball_direction = -ball_direction;
			ball_pos_y = 600 - 2*ball_radius;
		}
	}

	protected void Start(Object sender, EventArgs events)
	{
		if (paused)
		{
			animation_timer.Enabled = true;
			start_button.Text = "Pause";
			paused = false;
		}
		else
		{
			animation_timer.Enabled = false;
			start_button.Text = "Unpause";
			paused = true;
		}
	}

	protected void Reset(Object sender, EventArgs events)
	{
		// Initial Values
		paused = true;
		ball_speed = 0.0;
		ball_direction = 0.0;
		ball_pos_x = 400;
		ball_pos_y = 300;

		// Reset Timer
		animation_timer.Enabled = false;

		// Reset Start Button
		start_button.Text = "Start";
	}

	protected void Exit(Object sender, EventArgs events)
	{
		Close();
	}

	public class Graphic_Panel : Panel
	{
		private static Brush RedBrush    = new SolidBrush(Color.Red);
		private static Brush OrangeBrush = new SolidBrush(Color.Orange);
		private static Brush YellowBrush = new SolidBrush(Color.Yellow);
		private static Brush GreenBrush  = new SolidBrush(Color.Green);
		private static Pen GrayPen  = new Pen(Color.Gray, 2.0f);

		protected override void OnPaint(PaintEventArgs e)
		{
			int new_ball_pos_x = (int)Math.Round(ball_pos_x);
			int new_ball_pos_y = (int)Math.Round(ball_pos_y);
			int new_ball_radius = (int)Math.Round(ball_radius);
			Graphics graph = e.Graphics;
			graph.FillEllipse(RedBrush,new_ball_pos_x,new_ball_pos_y,2*new_ball_radius,2*new_ball_radius);
			base.OnPaint(e);
		}
	}
}
