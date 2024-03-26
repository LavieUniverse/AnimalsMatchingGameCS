namespace AnimalsMatchingGame
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            AnimalsButtons.IsVisible = false;
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalsButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;

            List<String> animalsEmoji = new List<String>()
            {
                "🐖", "🐖",
                "🦋", "🦋",
                "🐄", "🐄",
                "🐸", "🐸",
                "🦚", "🦚",
                "🦑", "🦑",
                "🐒", "🐒",
                "🐳", "🐳",
            };

            foreach (var button in AnimalsButtons.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(animalsEmoji.Count);
                string nextEmoji = animalsEmoji[index];
                button.Text = nextEmoji;
                animalsEmoji.RemoveAt(index);
            }

            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
        }
        
        int tenthsOfSecondsElapsed = 0;

        private bool TimerTick()
        {
            
            if (!this.IsLoaded)
                return false;
                tenthsOfSecondsElapsed++;
                TimeElapsed.Text = "Time elapsed:" + (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            
            
            if (PlayAgainButton.IsVisible)
            {
                tenthsOfSecondsElapsed = 0;
                return false;
            }
            return true;
        }

        Button lastClicked;
        bool findingMatch = false;
        int matchesFound;
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button buttonClicked)
            {
                if (!string.IsNullOrEmpty(buttonClicked.Text) && (findingMatch == false))
                {
                    buttonClicked.BackgroundColor = Colors.Red;
                    lastClicked = buttonClicked;
                    findingMatch = true;
                }
                else
                {
                    if ((buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text) && (buttonClicked.Text != ""))
                    {
                        matchesFound++;
                        lastClicked.Text = "";
                        buttonClicked.Text = "";
                    }
                    lastClicked.BackgroundColor = Colors.LightBlue;
                    buttonClicked.BackgroundColor = Colors.LightBlue;
                    findingMatch = false;
                }
            }

            if (matchesFound == 8) 
            {
                matchesFound = 0;
                AnimalsButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
            }
        }
    }
}

