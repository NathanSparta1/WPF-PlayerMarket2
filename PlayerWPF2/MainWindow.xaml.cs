using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlayerWPF2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Initialise();
        }
        Player player;
        PlayStyle playstyle;
        Agent agent;

        List<Player> players = new List<Player>();
        List<Agent> agents = new List<Agent>();
        List<PlayStyle> playstyles = new List<PlayStyle>();

        void Initialise()
        {
            using (var db = new PlayerMarketEntities())
            {
                players = db.Players.ToList();
                playstyles = db.PlayStyles.ToList();
            }
            ListBoxPlayers.ItemsSource = players;
            ListBoxPlayers.DisplayMemberPath = "PlayerName";
             ComboBoxPlayStyle.ItemsSource = playstyles;
            ComboBoxPlayStyle.DisplayMemberPath = "PlayStyleName";

        }

        private void ListBoxTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //print out details of selecgted item
            // instance = (convert to player) the item selected in listbox by user
            player = (Player)ListBoxPlayers.SelectedItem;
            if (player != null)
            {
                TextBoxID.Text = player.PlayerId.ToString();
                TextBoxName.Text = player.PlayerName;
                TextBoxPlayStyle.Text = player.PlayStyleId.ToString();
                TextBoxGamesPlayed.Text = player.Games.ToString();
                TextBoxGoals.Text = player.Goals.ToString();
                TextBoxCleanSheets.Text = player.CleanSheets.ToString();
                TextBoxAssists.Text = player.Assists.ToString();
                ButtonEdit.IsEnabled = true;
                ButtonDelete.IsEnabled = true;

                if (player.PlayStyleId != null)
                {
                    ComboBoxPlayStyle.SelectedIndex = (int)player.PlayStyleId - 1;
                }
                else
                {
                    ComboBoxPlayStyle.SelectedItem = null;
                }
            }

        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            
            if (ButtonEdit.Content.ToString() == "Edit")
            {
                TextBoxName.IsReadOnly = false;
                TextBoxPosition.IsReadOnly = false;
                TextBoxPlayStyle.IsReadOnly = false;
                TextBoxGamesPlayed.IsReadOnly = false;
                TextBoxGoals.IsReadOnly = false;
                TextBoxAssists.IsReadOnly = false;
                TextBoxCleanSheets.IsReadOnly = false;

                ButtonEdit.Content = "Save";

                TextBoxName.Background = Brushes.White;
                TextBoxPosition.Background = Brushes.White;
                TextBoxPlayStyle.Background = Brushes.White;
                TextBoxGamesPlayed.Background = Brushes.White;
                TextBoxGoals.Background = Brushes.White;
                TextBoxAssists.Background = Brushes.White;
                TextBoxCleanSheets.Background = Brushes.White;
            }
            else
            {
                using (var db = new PlayerMarketEntities())
                {
                    
                    var playerToEdit = db.Players.Find(player.PlayerId);
                    
                   
                    playerToEdit.PlayerName = TextBoxName.Text;  // add all fields
                    playerToEdit.Goals = Convert.ToInt32(TextBoxGoals.Text);
                    playerToEdit.Games = Convert.ToInt32(TextBoxGamesPlayed.Text);
                    playerToEdit.Assists = Convert.ToInt32(TextBoxAssists.Text);
                    playerToEdit.CleanSheets = Convert.ToInt32(TextBoxCleanSheets.Text);
                    playerToEdit.PlayStyleId = Convert.ToInt32(TextBoxPlayStyle.Text);

                    //converting categoryid to integer from text box (string)
                    // tryparse is a safe way to do conversion: null if fails
                    int.TryParse(TextBoxPlayStyle.Text, out int playestyleId);
                    playerToEdit.PlayStyleId = playestyleId;

                    if (player.PlayStyleId != null)
                    {
                        ComboBoxPlayStyle.SelectedIndex = (int)player.PlayStyleId - 1;
                    }
                    else
                    {
                        ComboBoxPlayStyle.SelectedItem = null;
                    }
                    // save to database
                    db.SaveChanges();

                    ListBoxPlayers.ItemsSource = null; //  reset list box
                    players = db.Players.ToList();       // get fresh link

                    ListBoxPlayers.ItemsSource = players; // re-link 
                }
                ButtonEdit.Content = "Edit";
                ButtonEdit.IsEnabled = false;

                TextBoxName.IsReadOnly = true;
                TextBoxPosition.IsReadOnly = true;
                TextBoxPlayStyle.IsReadOnly = true;
                TextBoxGamesPlayed.IsReadOnly = true;
                TextBoxGoals.IsReadOnly = true;
                TextBoxAssists.IsReadOnly = true;

                var brush = new BrushConverter();

                TextBoxName.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxPosition.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxPlayStyle.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxGamesPlayed.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxGoals.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxAssists.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxCleanSheets.Background = (Brush)brush.ConvertFrom("#E8FBFF");
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonAdd.Content.ToString() == "Add")
            {
                //set boxes to editable
                ButtonAdd.Content = "Confirm";
                TextBoxName.Background = Brushes.White;
                TextBoxPosition.Background = Brushes.White;
                TextBoxPlayStyle.Background = Brushes.White;
                TextBoxGamesPlayed.Background = Brushes.White;
                TextBoxGoals.Background = Brushes.White;
                TextBoxAssists.Background = Brushes.White;
                TextBoxCleanSheets.Background = Brushes.White;


                TextBoxName.IsReadOnly = false;
                TextBoxPosition.IsReadOnly = false;
                TextBoxPlayStyle.IsReadOnly = false;
                TextBoxGamesPlayed.IsReadOnly = false;
                TextBoxGoals.IsReadOnly = false;
                TextBoxAssists.IsReadOnly = false;
                TextBoxCleanSheets.IsReadOnly = false;


                //clear out boxes
                TextBoxName.Text = " ";
                TextBoxPosition.Text = " ";
                TextBoxPlayStyle.Text = " ";
                TextBoxGamesPlayed.Text = " ";
                TextBoxGoals.Text = " ";
                TextBoxAssists.Text = " ";
                TextBoxCleanSheets.Text = " ";

            }
            else
            {
                ButtonAdd.Content = "Add";
                TextBoxName.IsReadOnly = true;
                TextBoxPosition.IsReadOnly = true;
                TextBoxPlayStyle.IsReadOnly = true;
                TextBoxGamesPlayed.IsReadOnly = true;
                TextBoxGoals.IsReadOnly = true;
                TextBoxAssists.IsReadOnly = true;

                var brush = new BrushConverter();
                TextBoxName.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxPosition.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxPlayStyle.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxGamesPlayed.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxGoals.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxAssists.Background = (Brush)brush.ConvertFrom("#E8FBFF");

                using (var db = new PlayerMarketEntities())
                {
                    Player newPlayer = new Player
                    {
                        PlayerName = TextBoxName.Text,
                        Goals = Convert.ToInt32(TextBoxGoals.Text),
                        Games = Convert.ToInt32(TextBoxGamesPlayed.Text),
                        Assists = Convert.ToInt32(TextBoxAssists.Text),
                       
                        CleanSheets = Convert.ToInt32(TextBoxCleanSheets.Text),
                        PlayStyleId = Convert.ToInt32(TextBoxPlayStyle.Text)


                    };

                    db.Players.Add(newPlayer);

                    // save to database
                    db.SaveChanges();

                    ListBoxPlayers.ItemsSource = null; //  reset list box
                    players = db.Players.ToList();       // get fresh link

                    ListBoxPlayers.ItemsSource = players; // re-link 
                }
                ButtonAdd.Content = "Add";
                TextBoxName.IsReadOnly = true;
                TextBoxPosition.IsReadOnly = true;
                TextBoxPlayStyle.IsReadOnly = true;
                TextBoxGamesPlayed.IsReadOnly = true;
                TextBoxGoals.IsReadOnly = true;
                TextBoxAssists.IsReadOnly = true;
                TextBoxCleanSheets.IsReadOnly = true;

                brush = new BrushConverter();
                TextBoxName.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxPosition.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxPlayStyle.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxGamesPlayed.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxGoals.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxAssists.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxCleanSheets.Background = (Brush)brush.ConvertFrom("#E8FBFF");
            }


        }


        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonDelete.Content.ToString() == "Delete")
            {

                ButtonDelete.Content = "Are You Sure?";

                TextBoxName.Background = Brushes.White;
                TextBoxPosition.Background = Brushes.White;
                TextBoxPlayStyle.Background = Brushes.White;
                TextBoxGamesPlayed.Background = Brushes.White;
                TextBoxGoals.Background = Brushes.White;
                TextBoxAssists.Background = Brushes.White;
                TextBoxCleanSheets.Background = Brushes.White;

                TextBoxName.IsReadOnly = false;
                TextBoxPosition.IsReadOnly = false;
                TextBoxPlayStyle.IsReadOnly = false;
                TextBoxGamesPlayed.IsReadOnly = false;
                TextBoxGoals.IsReadOnly = false;
                TextBoxAssists.IsReadOnly = false;
                TextBoxCleanSheets.IsReadOnly = false;

                TextBoxName.Background = Brushes.Red;
                TextBoxPosition.Background = Brushes.Red;
                TextBoxPlayStyle.Background = Brushes.Red;
                TextBoxGamesPlayed.Background = Brushes.Red;
                TextBoxGoals.Background = Brushes.Red;
                TextBoxAssists.Background = Brushes.Red;
                TextBoxCleanSheets.Background = Brushes.Red;
            }
            else
            {

                using (var db = new PlayerMarketEntities())
                {


                    
                    var playerToDelete = db.Players.Find(player.PlayerId);
                    db.Players.Remove(playerToDelete);

                    db.SaveChanges();

                    ListBoxPlayers.ItemsSource = null; //  reset list box
                    players = db.Players.ToList();       // get fresh link

                    ListBoxPlayers.ItemsSource = players; // re-link 

                }

                ButtonDelete.Content = "Delete";
                ButtonDelete.IsEnabled = false;

                //clear out boxes
                TextBoxName.Text = " ";
                TextBoxPosition.Text = " ";
                TextBoxPlayStyle.Text = " ";
                TextBoxGamesPlayed.Text = " ";
                TextBoxGoals.Text = " ";
                TextBoxAssists.Text = " ";
                TextBoxCleanSheets.Text = " ";

                var brush = new BrushConverter();
                TextBoxName.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxPosition.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxPlayStyle.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxGamesPlayed.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxGoals.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxAssists.Background = (Brush)brush.ConvertFrom("#E8FBFF");
                TextBoxCleanSheets.Background = (Brush)brush.ConvertFrom("#E8FBFF");
            }
        }

        private void ListBoxTasks_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
