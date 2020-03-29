using System;
using CodingCampusCSharpHomework;

namespace HomeworkTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<Task3, string> TaskSolver = task =>
            {
                const int radiusInKm = 6371;
                const int latitudeIndex = 3; 
                const int longitudeIndex = 2; 
                const int AddressIndex = 1; 
                const int NameIndex = 0; 
                string UserLongitude = task.UserLongitude;
                string UserLatitude = task.UserLatitude;
                int placesAmount = task.DefibliratorStorages.Length;
                UserLongitude = UserLongitude.Replace(',', '.');
                UserLatitude = UserLatitude.Replace(',', '.');
                if (!double.TryParse(UserLongitude, out double longitudeDoubleUser)
                        || !double.TryParse(UserLatitude, out double latitudeDoubleUser))
                {
                    return "Wrong data";
                }
                double nearest = double.MaxValue;
                string nearestAddress = "";
                string nearestName = "";
                for (int i = 0; i < placesAmount; i++)
                {
                    string defibliratorStorage = task.DefibliratorStorages[i];
                    string[] parseInformation = defibliratorStorage.Split(';');
                    string latitideString = parseInformation[latitudeIndex];
                    string longitudeString = parseInformation[longitudeIndex];
                    longitudeString = longitudeString.Replace(',', '.');
                    latitideString = latitideString.Replace(',', '.');
                    if (!double.TryParse(latitideString, out double latitideDouble)
                        || !double.TryParse(longitudeString, out double longitudeDouble))
                    {
                        return "Wrong data";
                    }
                    double x = (longitudeDouble - longitudeDoubleUser) * Math.Cos((latitudeDoubleUser + latitideDouble)/2);
                    double y = (latitideDouble - latitudeDoubleUser);
                    double d = Math.Sqrt(x * x + y * y) * radiusInKm;
                    if (d < nearest)
                    {
                        nearestName = parseInformation[NameIndex];
                        nearestAddress = parseInformation[AddressIndex];
                        nearest = d;
                    }
                }   
                return $"Name: {nearestName}; Address: {nearestAddress}";
            };

            Task3.CheckSolver(TaskSolver);
        }
    }
}
