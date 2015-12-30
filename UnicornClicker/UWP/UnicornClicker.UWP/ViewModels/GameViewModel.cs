using System;
using System.ComponentModel;
using UnicornClicker.UWP.Models;
using Windows.UI.Xaml;

namespace UnicornClicker.UWP.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private static readonly int DEFAULT_GAME_TIME = 5;
        private static readonly int DEFAULT_COUNTDOWN_TIME = 3;

        private DispatcherTimer _countdownTimer;
        private int? _countdownTimeRemaining;
        private DispatcherTimer _gameTimer;
        private int? _gameTimeRemaining;
        private bool _playing;
        private int _clickCount;
        private Visibility _navigationVisibility = Visibility.Visible;

        public event PropertyChangedEventHandler PropertyChanged;

        public int? CountdownTimeRemaining
        {
            get
            {
                return _countdownTimeRemaining;
            }
            set
            {
                _countdownTimeRemaining = value;
                OnPropertyChanged(nameof(CountdownTimeRemaining));
                OnPropertyChanged(nameof(CountdownText));
            }
        }

        public string CountdownText
        {
            get
            {
                return CountdownTimeRemaining == null
                    ? null
                    : CountdownTimeRemaining + "...";
            }
        }

        public int? GameTimeRemaining
        {
            get
            {
                return _gameTimeRemaining;
            }
            set
            {
                _gameTimeRemaining = value;
                OnPropertyChanged(nameof(GameTimeRemaining));
                OnPropertyChanged(nameof(GameTimeText));
            }
        }

        public string GameTimeText
        {
            get
            {
                return GameTimeRemaining == null
                    ? "Time's up!"
                    : GameTimeRemaining + " seconds left to play";
            }
        }

        public bool Playing
        {
            get
            {
                return _playing;
            }
            set
            {
                _playing = value;
                OnPropertyChanged(nameof(Playing));
                OnPropertyChanged(nameof(UnicornOpacity));
            }
        }

        public double UnicornOpacity
        {
            get
            {
                return Playing
                    ? 1
                    : 0.5;
            }
        }

        public int ClickCount
        {
            get
            {
                return _clickCount;
            }
            set
            {
                _clickCount = value;
                OnPropertyChanged(nameof(ClickCount));
                OnPropertyChanged(nameof(ClickCountText));
            }
        }

        public string ClickCountText
        {
            get
            {
                return ClickCount == 0
                    ? "Click or tap the unicorn to score"
                    : ClickCount + " clicks";
            }
        }

        public Visibility NavigationVisibility
        {
            get
            {
                return _navigationVisibility;
            }
            set
            {
                _navigationVisibility = value;
                OnPropertyChanged(nameof(NavigationVisibility));
            }
        }

        public void StartNewGame()
        {
            NavigationVisibility = Visibility.Collapsed;

            ClickCount = 0;
            CountdownTimeRemaining = DEFAULT_COUNTDOWN_TIME;
            GameTimeRemaining = DEFAULT_GAME_TIME;

            _gameTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _gameTimer.Tick += GameTimerTick;

            _countdownTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _countdownTimer.Tick += CountdownTimerTick;

            _countdownTimer.Start();
        }

        public void HandleClick()
        {
            if (_playing)
            {
                ClickCount++;
            }
        }

        private void CountdownTimerTick(object sender, object e)
        {
            if (CountdownTimeRemaining != null)
            {
                if (CountdownTimeRemaining <= 1)
                {
                    _countdownTimer.Stop();
                    CountdownTimeRemaining = null;
                    Playing = true;
                    _gameTimer.Start();
                }
                else
                {
                    CountdownTimeRemaining--;
                }
            }
        }

        private void GameTimerTick(object sender, object e)
        {
            if (GameTimeRemaining != null)
            {
                if (GameTimeRemaining <= 1)
                {
                    Playing = false;
                    _gameTimer.Stop();
                    GameTimeRemaining = null;
                    RecordGame(DEFAULT_GAME_TIME, _clickCount);
                    NavigationVisibility = Visibility.Visible;
                }
                else
                {
                    GameTimeRemaining--;
                }
            }
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private static void RecordGame(int duration, int clicks)
        {
            using (var db = new UnicornClickerContext())
            {
                db.GameScores.Add(new GameScore
                {
                    Clicks = clicks,
                    Duration = duration,
                    ClicksPerSecond = (double)clicks / duration,
                    Played = DateTime.Now
                });

                db.SaveChanges();
            }
        }
    }
}
