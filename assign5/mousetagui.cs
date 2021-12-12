using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class MouseTagUI : Form
{
	private Panel header_panel = new Panel();
	private Graphic_Panel display_panel = new Graphic_Panel();
	private Panel control_panel = new Panel();

	private Label title_label = new Label();
	private Label tagged_label = new Label();
	private Label total_label = new Label();
	private Label percent_label = new Label();

	private TextBox tagged_output = new TextBox();
	private TextBox total_output = new TextBox();
	private TextBox percent_output = new TextBox();

	private Button go_button = new Button();
	private Button pause_button = new Button();
	private Button exit_button = new Button();

	private Size max_win_size = new Size(800,900);
	private Size min_win_size = new Size(800,900);

	private bool paused;
	private bool running;

	private double mouse_speed;
	private double mouse_direction;
	static private double mouse_pos_x;
	static private double mouse_pos_y;

	private static int mouse_radius = 15;

	private System.Timers.Timer animation_timer = new System.Timers.Timer();
	private double animation_rate_hz = 60;

	private System.Timers.Timer refresh_timer = new System.Timers.Timer();
	private double refresh_rate_hz = 60;

	private Random rng = new Random();

	public MouseTagUI()
	{
		// Window Sizes
		MaximumSize = max_win_size;
		MinimumSize = min_win_size;

		// Text
		Text = "Mouse Tag";
		title_label.Text = "Mouse Tag by Zachary Thompson";
		tagged_label.Text = "Tagged";
		total_label.Text = "Total";
		percent_label.Text = "Percent";
		go_button.Text = "Go";
		pause_button.Text = "Pause";
		exit_button.Text = "Exit";
		
		// Sizes
		Size = MinimumSize;
		header_panel.Size = new Size(800,100);
		display_panel.Size = new Size(800,700);
		control_panel.Size = new Size(800,100);
		title_label.Size = new Size(500,50);
		tagged_label.Size = new Size(75,25);
		total_label.Size = new Size(75,25);
		percent_label.Size = new Size(75,25);
		tagged_output.Size = new Size(75,50);
		total_output.Size = new Size(75,50);
		percent_output.Size = new Size(75,50);
		go_button.Size = new Size(80,70);
		pause_button.Size = new Size(80,70);
		exit_button.Size = new Size(80,70);

		// Colors
		header_panel.BackColor = Color.Yellow;
		display_panel.BackColor = Color.LightGreen;
		control_panel.BackColor = Color.LightBlue;
		title_label.ForeColor = Color.Black;
		tagged_label.ForeColor = Color.Black;
		total_label.ForeColor = Color.Black;
		percent_label.ForeColor = Color.Black;
		tagged_label.BackColor = Color.White;
		total_label.BackColor = Color.White;
		percent_label.BackColor = Color.White;
		tagged_output.ForeColor = Color.Black;
		total_output.ForeColor = Color.Black;
		percent_output.ForeColor = Color.Black;
		tagged_output.BackColor = Color.White;
		total_output.BackColor = Color.White;
		percent_output.BackColor = Color.White;
		go_button.BackColor = Color.Green;
		go_button.ForeColor = Color.Black;
		pause_button.BackColor = Color.Orange;
		pause_button.ForeColor = Color.Black;
		exit_button.BackColor = Color.Red;
		exit_button.ForeColor = Color.Black;

		// Fonts
		title_label.Font = new Font("Arial",18,FontStyle.Regular);
		tagged_label.Font = new Font("Arial",12,FontStyle.Regular);
		total_label.Font = new Font("Arial",12,FontStyle.Regular);
		percent_label.Font = new Font("Arial",12,FontStyle.Regular);
		tagged_output.Font = new Font("Arial",18,FontStyle.Regular);
		total_output.Font = new Font("Arial",18,FontStyle.Regular);
		percent_output.Font = new Font("Arial",18,FontStyle.Regular);
		go_button.Font = new Font("Arial",18,FontStyle.Regular);
		pause_button.Font = new Font("Arial",18,FontStyle.Regular);
		exit_button.Font = new Font("Arial",18,FontStyle.Regular);

		// Locations
		header_panel.Location = new Point(0,0);
		display_panel.Location = new Point(0,100);
		control_panel.Location = new Point(0,800);
		title_label.Location = new Point(200,30);
		tagged_label.Location = new Point(200,10);
		total_label.Location = new Point(285,10);
		percent_label.Location = new Point(370,10);
		tagged_output.Location = new Point(200,35);
		total_output.Location = new Point(285,35);
		percent_output.Location = new Point(370,35);
		go_button.Location = new Point(10,10);
		pause_button.Location = new Point(525,10);
		exit_button.Location = new Point(650,10);

		// Enter key presses go
		AcceptButton = go_button;

		// Add Controls
		Controls.Add(header_panel);
		header_panel.Controls.Add(title_label);
		Controls.Add(display_panel);
		Controls.Add(control_panel);
		control_panel.Controls.Add(go_button);
		control_panel.Controls.Add(tagged_label);
		control_panel.Controls.Add(tagged_output);
		control_panel.Controls.Add(total_label);
		control_panel.Controls.Add(total_output);
		control_panel.Controls.Add(percent_label);
		control_panel.Controls.Add(percent_output);
		control_panel.Controls.Add(pause_button);
		control_panel.Controls.Add(exit_button);

		// Event Handlers
		go_button.Click += new EventHandler(Start_Game);
		pause_button.Click += new EventHandler(Pause_Game);
		exit_button.Click += new EventHandler(Exit);

		// Initial State
		tagged_output.Text = "0";
		total_output.Text = "0";
		percent_output.Text = "0.0";
		mouse_speed = 0;
		mouse_direction = 0;
		paused = true;
		mouse_pos_x = 350;
		mouse_pos_y = 350;

		// Setup Timers
		animation_timer.Enabled = false;
		animation_timer.Interval = (int)Math.Round(1000.0/animation_rate_hz); // Convert Hz -> ms
		animation_timer.Elapsed += new ElapsedEventHandler(MoveMouse);
		refresh_timer.Enabled = false;
		refresh_timer.Interval = (int)Math.Round(1000.0/refresh_rate_hz);
		refresh_timer.Elapsed += new ElapsedEventHandler(Refresh);

		// Calls OnPaint()
		display_panel.Invalidate();

	}

	protected void Refresh(Object sender, ElapsedEventArgs events)
	{
	}

	protected void MoveMouse(Object sender, ElapsedEventArgs events)
	{
		// Convert from pix/s to pix/tic
		double converted_speed = mouse_speed * (animation_timer.Interval / 1000);

		mouse_pos_x = mouse_pos_x + (converted_speed * Math.Cos(mouse_direction));
		mouse_pos_y = mouse_pos_y + (converted_speed * Math.Sin(mouse_direction));

		if( mouse_pos_x < 0 ||
			mouse_pos_y < 0 ||
			mouse_pos_x + 2*mouse_radius > 800 ||
			mouse_pos_y + 2*mouse_radius > 700 )
		{
			mouse_pos_x = 350;
			mouse_pos_y = 350;
			// Generate random direction between 0 and 2pi
			mouse_direction = rng.NextDouble() * Math.PI * 2;
			// Generate random speed between 0 and 50
			mouse_speed = rng.NextDouble() * 50;
		}

		// Calls OnPaint()
		display_panel.Invalidate();
	}

	protected void Start_Game(Object sender, EventArgs events)
	{
		// Generate random direction between 0 and 2pi
		mouse_direction = rng.NextDouble() * Math.PI * 2;
		// Generate random speed between 0 and 50
		mouse_speed = rng.NextDouble() * 100;

		// Start Timers
		animation_timer.Enabled = true;
		refresh_timer.Enabled = true;
	}

	protected void Pause_Game(Object sender, EventArgs events)
	{
		paused = true;
		// Start Timers
		animation_timer.Enabled = false;
		refresh_timer.Enabled = false;
	}

	protected void Exit(Object sender, EventArgs events)
	{
		Close();
	}

	public class Graphic_Panel : Panel
	{
		private static Brush GrayBrush  = new SolidBrush(Color.Gray);
		private static int display_mouse_pos_x;
		private static int display_mouse_pos_y;

		protected override void OnMouseDown(MouseEventArgs me)  
		{
			if (me.X >= display_mouse_pos_x &&
				me.X < display_mouse_pos_x + 2*mouse_radius &&
				me.Y >= display_mouse_pos_y &&
				me.Y < display_mouse_pos_y + 2*mouse_radius)
			{
				System.Console.WriteLine("In OnMouseDown");
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			display_mouse_pos_x = (int)Math.Round(mouse_pos_x);
			display_mouse_pos_y = (int)Math.Round(mouse_pos_y);
			Graphics graph = e.Graphics;
			graph.FillEllipse(GrayBrush,display_mouse_pos_x,display_mouse_pos_y,2*mouse_radius,2*mouse_radius);
			base.OnPaint(e);
		}
	}
}
