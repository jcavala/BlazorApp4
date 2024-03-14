using BlazorApp4.Data;
using System.Text;
using System.Text.Json;

namespace BlazorApp4.Jobs
{
    public class Graph
    {

        public List<int> counts { get; set; } = new();
        public List<decimal> sums { get; set; } = new();
        public List<decimal> maxPrices { get; set; } = new();
        public List<decimal> avgPrices { get; set; } = new();

        public List<string> categories { get; set; } = new();
        
        public string json()
        {
            return JsonSerializer.Serialize(this);
        }

        public string create()
        {
            return  "<!DOCTYPE html>" +
                "\r\n<html>\r\n" +
                "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js\"></script>" +
                "\r\n<body>\r\n\r\n<canvas id=\"myChart\" style=\"width:100%;max-width:600px\"></canvas>\r\n" +
                "<canvas id=\"secondChart\" style=\"width:100%;max-width:600px\"></canvas>\r\n\r\n" +
                "<canvas id=\"thirdChart\" style=\"width:100%;max-width:600px\"></canvas>\r\n\r\n" +
                "<script>\r\n" +
                $"var chartData = {json()};\r\n" +
                "var barColors = \"red\";\r\n\r\n" +
                "new Chart(\"myChart\", {\r\n  " +
                "type: \"bar\",\r\n  data: {\r\n    " +
                "labels: chartData.categories,\r\n    " +
                "datasets: [{\r\n      " +
                "backgroundColor: barColors,\r\n      " +
                "data: chartData.counts\r\n    }]\r\n  },\r\n  " +
                "options: {\r\n    legend: {display: false}," +
                "\r\n    title: {\r\n      display: true,\r\n      " +
                "text: \"Brojnost proizvoda po kategoriji\"\r\n    },\r\n " +
                "\r\n scales: {\r\n  yAxes: " +
                "[{\r\n   display: true,\r\n " +
                "ticks: {\r\n    beginAtZero: true  \r\n  }\r\n   }]\r\n } }\r\n});\r\n\r\n" +
                "new Chart(\"secondChart\", {\r\n  " +
                "type: \"bar\",\r\n  data: {\r\n    " +
                "labels: chartData.categories,\r\n    " +
                "datasets: [{\r\n      " +
                "backgroundColor: barColors,\r\n      " +
                "data: chartData.avgPrices\r\n    }]\r\n  },\r\n  " +
                "options: {\r\n    legend: {display: false}," +
                "\r\n    title: {\r\n      display: true,\r\n      " +
                "text: \"Prosječna cijena proizvoda po kategoriji\"\r\n    },\r\n " +
                "\r\n scales: {\r\n  yAxes: " +
                "[{\r\n   display: true,\r\n " +
                "ticks: {\r\n    beginAtZero: true  \r\n  }\r\n   }]\r\n } }\r\n});\r\n\r\n" +
                "new Chart(\"thirdChart\", {\r\n  " +
                "type: \"bar\",\r\n  data: {\r\n    " +
                "labels: chartData.categories,\r\n    " +
                "datasets: [{\r\n      " +
            "backgroundColor: barColors,\r\n      " +
        "data: chartData.sums\r\n    }]\r\n  },\r\n  " +
        "options: {\r\n    legend: {display: false}," +
        "\r\n    title: {\r\n      display: true,\r\n      " +
        "text: \"Ukupna vrijednost proizvoda po kategoriji\"\r\n    },\r\n " +
        "\r\n scales: {\r\n  yAxes: " +
        "[{\r\n   display: true,\r\n " +
        "ticks: {\r\n    beginAtZero: true  \r\n  }\r\n   }]\r\n } }\r\n});\r\n\r\n" +
                "</script>" +
                "\r\n\r\n</body>\r\n</html>";
        }
    }
}
