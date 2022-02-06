var options = {
  series: [40, 50, 34],
  labels: ["Correct", "Skipped", "Wrong"],
  markers: {
    colors: ["#dddd", "#E91E63", "#9C27B0"],
  },
  chart: {
    type: "pie",
  },
  responsive: [
    {
      breakpoint: 480,
      options: {
        chart: {
          width: 300,
        },
        legend: {
          position: "bottom",
        },
      },
    },
  ],
  legend: {
    position: "bottom",
    offsetX: -40,
  },
  dataLabels: {
    enabled: false,
  },
};

var chart = new ApexCharts(document.querySelector("#chart"), options);
chart.render();
// var options = {
//   series: [44, 55, 13],
//   chart: {
//     width: 380,
//     type: "pie",
//   },
//   labels: ["Correct", "Skipped", "Wrong"],
//   markers: {
//     colors: ["#dddd", "#E91E63", "#9C27B0"],
//   },
//   responsive: [
//     {
//       breakpoint: 480,
//       options: {
//         chart: {
//           width: 200,
//         },
//         legend: {
//           position: "bottom",
//         },
//       },
//     },
//   ],
// };

// var chart = new ApexCharts(document.querySelector("#chart"), options);
// chart.render();

// chart 2
var options = {
  series: [
    {
      name: "Correct",
      data: [44, 55, 41, 67, 22, 43],
    },
    {
      name: "Skipped",
      data: [13, 23, 20, 8, 13, 27],
    },
    {
      name: "Wrong",
      data: [11, 17, 15, 15, 21, 14],
    },
  ],

  chart: {
    type: "bar",
    height: 350,
    stacked: true,
    toolbar: {
      show: false,
    },
    zoom: {
      enabled: false,
    },
  },

  responsive: [
    {
      breakpoint: 480,
      options: {
        legend: {
          position: "bottom",
          offsetX: -10,
          offsetY: 0,
        },
      },
    },
  ],
  plotOptions: {
    bar: {
      horizontal: false,
      borderRadius: 10,
    },
  },
  xaxis: {
    categories: [
      "উচ্চতর গণিত",
      "রসায়ন",
      "পদার্থবিজ্ঞান",
      "জীববিজ্ঞান",
      "সাধারণ জ্ঞান",
      "English",
      "বাংলা",
    ],
  },
  yaxis: {
    tickAmount: 10,
  },
  legend: {
    position: "bottom",
    offsetX: 40,
  },
  fill: {
    opacity: 1,
  },
  dataLabels: {
    enabled: false,
  },
};

var chart = new ApexCharts(document.querySelector("#chart1"), options);
chart.render();

// chart 3
var options = {
  series: [
    {
      name: "Correct",
      data: [44, 55, 41, 67, 22, 43],
    },
    {
      name: "Skipped",
      data: [13, 23, 20, 8, 13, 27],
    },
    {
      name: "Wrong",
      data: [11, 17, 15, 15, 21, 14],
    },
  ],

  chart: {
    type: "bar",
    height: 350,
    stacked: true,
    toolbar: {
      show: false,
    },
    zoom: {
      enabled: false,
    },
  },

  responsive: [
    {
      breakpoint: 480,
      options: {
        legend: {
          position: "bottom",
          offsetX: -10,
          offsetY: 0,
        },
      },
    },
  ],
  plotOptions: {
    bar: {
      horizontal: false,
      borderRadius: 10,
    },
  },
  xaxis: {
    categories: [
      "উচ্চতর গণিত",
      "রসায়ন",
      "পদার্থবিজ্ঞান",
      "জীববিজ্ঞান",
      "সাধারণ জ্ঞান",
      "English",
      "বাংলা",
    ],
  },
  yaxis: {
    tickAmount: 10,
  },
  legend: {
    position: "bottom",
    offsetX: 40,
  },
  fill: {
    opacity: 1,
  },
  dataLabels: {
    enabled: false,
  },
};

var chart = new ApexCharts(document.querySelector("#chart3"), options);
chart.render();
// chart 4
var options = {
  series: [
    {
      name: "Correct",
      data: [44, 55, 41, 67, 22, 43],
    },
    {
      name: "Skipped",
      data: [13, 23, 20, 8, 13, 27],
    },
    {
      name: "Wrong",
      data: [11, 17, 15, 15, 21, 14],
    },
  ],

  chart: {
    type: "bar",
    height: 350,
    stacked: true,
    toolbar: {
      show: false,
    },
    zoom: {
      enabled: false,
    },
  },

  responsive: [
    {
      breakpoint: 480,
      options: {
        legend: {
          position: "bottom",
          offsetX: -10,
          offsetY: 0,
        },
      },
    },
  ],
  plotOptions: {
    bar: {
      horizontal: false,
      borderRadius: 10,
    },
  },
  xaxis: {
    categories: [
      "উচ্চতর গণিত",
      "রসায়ন",
      "পদার্থবিজ্ঞান",
      "জীববিজ্ঞান",
      "সাধারণ জ্ঞান",
      "English",
      "বাংলা",
    ],
  },
  yaxis: {
    tickAmount: 10,
  },
  legend: {
    position: "bottom",
    offsetX: 40,
  },
  fill: {
    opacity: 1,
  },
  dataLabels: {
    enabled: false,
  },
};

var chart = new ApexCharts(document.querySelector("#chart4"), options);
chart.render();
// chart 5
var options = {
  series: [
    {
      name: "Correct",
      data: [44, 55, 41, 67, 22, 43],
    },
    {
      name: "Skipped",
      data: [13, 23, 20, 8, 13, 27],
    },
    {
      name: "Wrong",
      data: [11, 17, 15, 15, 21, 14],
    },
  ],

  chart: {
    type: "bar",
    height: 350,
    stacked: true,
    toolbar: {
      show: false,
    },
    zoom: {
      enabled: false,
    },
  },

  responsive: [
    {
      breakpoint: 480,
      options: {
        legend: {
          position: "bottom",
          offsetX: -10,
          offsetY: 0,
        },
      },
    },
  ],
  plotOptions: {
    bar: {
      horizontal: false,
      borderRadius: 10,
    },
  },
  xaxis: {
    categories: [
      "উচ্চতর গণিত",
      "রসায়ন",
      "পদার্থবিজ্ঞান",
      "জীববিজ্ঞান",
      "সাধারণ জ্ঞান",
      "English",
      "বাংলা",
    ],
  },
  yaxis: {
    tickAmount: 10,
  },
  legend: {
    position: "bottom",
    offsetX: 40,
  },
  fill: {
    opacity: 1,
  },
  dataLabels: {
    enabled: false,
  },
};

var chart = new ApexCharts(document.querySelector("#chart5"), options);
chart.render();
