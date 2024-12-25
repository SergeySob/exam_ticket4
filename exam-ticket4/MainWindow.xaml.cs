using Npgsql;
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

namespace exam_ticket4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class Data
        {
            public string date { get; set; }
            public string temp_min { get; set; }
            public string temp_max { get; set; }
            public string feels_like { get; set; }
            public string water_temp { get; set; }
            public string humidity { get; set; }
            public string pressure { get; set; }
            public string wind_speed { get; set; }
            public string wind_direction { get; set; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var data = new List<Data>();
            using (var con = new NpgsqlConnection("Host=ep-raspy-feather-a5jddzv9.us-east-2.aws.neon.tech;Database=ForExam;Username=ForExam_owner;Password=5McebrxlDGK9"))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM weather.weather_data;", con))
                {
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int year = (int)rdr["year"];
                        int month = (int)rdr["month"];
                        int day = (int)rdr["day"];
                        int temp_lo = (int)rdr["temp_lo"];
                        int temp_hi = (int)rdr["temp_hi"];
                        int feels_like = (int)rdr["feels_like"];
                        int water_temp = (int)rdr["water_temp"];
                        int humidity = (int)rdr["humidity"];
                        int pressure = (int)rdr["pressure"];
                        int wind_speed = (int)rdr["wind_speed"];
                        string wind_direction = (string)rdr["wind_direction"];

                        data.Add(new Data
                        {
                            date = $"{day}-{month}-{year}",
                            temp_min = temp_lo.ToString(),
                            temp_max = temp_hi.ToString(),
                            feels_like = feels_like.ToString(),
                            water_temp = water_temp.ToString(),
                            humidity = humidity.ToString(),
                            pressure = pressure.ToString(),
                            wind_speed = wind_speed.ToString(),
                            wind_direction = wind_direction.ToString()
                        });
                    }
                }
            }

            weatherGrid.ItemsSource = data;
        }
    }
}
