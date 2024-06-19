# Xamarin.Forms Chart Application

This project demonstrates how to create various types of charts (line, bar, pie, grouped bar, rectangle, and bitmap) using the OxyPlot library in a Xamarin.Forms application. It includes handling for user interactions such as selecting elements in the chart and zooming in/out.

## Features

- Line Chart with area fill
- Bar Chart
- Pie Chart
- Grouped Bar Chart
- Rectangle Chart
- Bitmap Chart
- User interaction (element selection)
- Zoom in/out functionality
- Legend display for all charts

## Requirements

- Xamarin.Forms
- OxyPlot.Xamarin.Forms

## Setup

1. Clone the repository:
    ```sh
    git clone [https://github.com/your-username/your-repository.git](https://github.com/mahdimasoudi/OxyplotExamples.git)
    ```
2. Open the solution in Visual Studio.
3. Restore the NuGet packages.

## Usage

### MainPage.cs

This file contains the main logic for displaying and interacting with the charts.

#### Fields and Constructor

- `currentPlotModel`: Holds the current `PlotModel` being displayed.
- `plotController`: Manages interactions with the plot view.

#### ConfigurePlotView Method

Configures the `PlotView` to display the provided `PlotModel` and binds touch and zoom events for user interactions.

#### HandleSeriesHit Method

Handles hit test results for different types of series, displaying an alert with details about the selected element.

#### CreateLineChart Method

Creates a `PlotModel` for a line chart with an `AreaSeries` to fill the area under the line.

#### CreateBarChart Method

Creates a `PlotModel` for a bar chart.

#### CreatePieChart Method

Creates a `PlotModel` for a pie chart.

#### CreateGroupedBarChart Method

Creates a `PlotModel` for a grouped bar chart.

#### CreateBitmapChart Method

Creates a `PlotModel` for a bitmap chart using an `ImageSeries`.

#### AddLegend Method

Adds a legend to the `PlotModel`.

#### OnShow*ChartClicked Methods

Event handlers for button clicks to display the respective charts by calling `ConfigurePlotView` with the created chart.

#### Zoom Methods

Handles zoom in and zoom out actions.

## Example

```csharp
private void OnShowLineChartClicked(object sender, EventArgs e)
{
    var lineChart = CreateLineChart();
    ConfigurePlotView(lineChart);
}

private void OnShowBarChartClicked(object sender, EventArgs e)
{
    var barChart = CreateBarChart();
    ConfigurePlotView(barChart);
}

private void OnZoomInClicked(object sender, EventArgs e)
{
    Zoom(1 / 1.5);
}

private void OnZoomOutClicked(object sender, EventArgs e)
{
    Zoom(1.5);
}
