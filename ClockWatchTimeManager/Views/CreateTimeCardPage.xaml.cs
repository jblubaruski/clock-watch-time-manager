using ClockWatchTimeManager.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;
using System.Resources;
using System.Security.AccessControl;

namespace ClockWatchTimeManager.Views;

public partial class CreateTimeCardPage : ContentPage
{
    // styles
    private Style? HorizontalSeparatorStyle { get; set; } = null;
    private Style? LabelStyle { get; set; } = null;
    private Style? DescriptionBorderStyle { get; set; } = null;
    private Style? DescriptionLabelStyle { get; set; } = null;
    private Style? ButtonStyle { get; set; } = null;

    // named controls
    private Label? currentBreakDescriptionValue = null;
    private Border? currentBreakDescriptionBorder = null;
    private Entry? currentBreakDescriptionEntryBox = null;

    public CreateTimeCardPage()
    {
        InitializeComponent();
        LoadStyles();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ((CreateTimeCardViewModel)BindingContext).AddBreakEntryBoxRequested += AddBreakEntryBox;
        ((CreateTimeCardViewModel)BindingContext).AddBreakDisplayBoxRequested += AddBreakDisplayBox;
        ((CreateTimeCardViewModel)BindingContext).ClearBreakDisplayRequested += ClearBreakDisplay;
        ((CreateTimeCardViewModel)BindingContext).SwitchBreakDescriptionLabelWithEntryRequested += SwitchBreakDescriptionLabelWithEntry;
        ((CreateTimeCardViewModel)BindingContext).SwitchBreakDescriptionEntryWithLabelRequested += SwitchBreakDescriptionEntryWithLabel;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ((CreateTimeCardViewModel)BindingContext).AddBreakEntryBoxRequested -= AddBreakEntryBox;
        ((CreateTimeCardViewModel)BindingContext).AddBreakDisplayBoxRequested -= AddBreakDisplayBox;
        ((CreateTimeCardViewModel)BindingContext).ClearBreakDisplayRequested -= ClearBreakDisplay;
        ((CreateTimeCardViewModel)BindingContext).SwitchBreakDescriptionLabelWithEntryRequested -= SwitchBreakDescriptionLabelWithEntry;
        ((CreateTimeCardViewModel)BindingContext).SwitchBreakDescriptionEntryWithLabelRequested -= SwitchBreakDescriptionEntryWithLabel;
    }

    // loads each style used in this code-behind file and logs an error in the log if the style could not be found
    private void LoadStyles()
    {
        if (Application.Current != null)
        {
            string key = "horizontalSeparatorStyle";
            if (Application.Current.Resources.TryGetValue(key, out object value) && value is Style)
                HorizontalSeparatorStyle = (Style)value;
            else
                LogLoadResourceError(nameof(LoadStyles), key, nameof(Style));

            key = "labelStyle";
            if (Application.Current.Resources.TryGetValue(key, out value) && value is Style)
                LabelStyle = (Style)value;
            else
                LogLoadResourceError(nameof(LoadStyles), key, nameof(Style));

            key = "descriptionBorderStyle";
            if (Application.Current.Resources.TryGetValue(key, out value) && value is Style)
                DescriptionBorderStyle = (Style)value;
            else
                LogLoadResourceError(nameof(LoadStyles), key, nameof(Style));

            key = "descriptionLabelStyle";
            if (Application.Current.Resources.TryGetValue(key, out value) && value is Style)
                DescriptionLabelStyle = (Style)value;
            else
                LogLoadResourceError(nameof(LoadStyles), key, nameof(Style));

            key = "buttonStyle";
            if (Application.Current.Resources.TryGetValue(key, out value) && value is Style)
                ButtonStyle = (Style)value;
            else
                LogLoadResourceError(nameof(LoadStyles), key, nameof(Style));
        }
    }

    private static void LogLoadResourceError(string errorLocation, string resourceKey, string resourceType)
    {
        App.logger.LogEntry
            (
                $"In {errorLocation}:\n",
                $"{resourceKey} does not exist as a {resourceType} in the resource dictionary"
            );
    }

    // adds a display box to breakDisplay
    private void AddBreakDisplayBox(string breakStartTime, string breakEndTime, string? description)
    {
        // break entry container

        Grid entryContainer = new()
        {
            RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition()
            },
            ColumnDefinitions =
            {
                new ColumnDefinition(new GridLength(80)),
                new ColumnDefinition(new GridLength(100)),
                new ColumnDefinition(new GridLength(280))
            }
        };

        // top border

        BoxView topBorder = new()
        {
            Style = HorizontalSeparatorStyle
        };
        entryContainer.AddWithSpan(topBorder, 0, 0, 1, 3);

        // start time

        Label startTimeLabel = new()
        {
            Text = "Start Time:",
            Style = LabelStyle,
            HorizontalOptions = LayoutOptions.End,
            Margin = new(0, 5, 0, 0)
        };
        entryContainer.AddWithSpan(startTimeLabel, 1, 0);

        Label startTimeValue = new()
        {
            Text = breakStartTime,
            Style = LabelStyle,
            Margin = new(5, 5, 0, 0)
        };
        entryContainer.AddWithSpan(startTimeValue, 1, 1, 1, 2);

        // end time

        Label endTimeLabel = new()
        {
            Text = "End Time:",
            Style = LabelStyle,
            HorizontalOptions = LayoutOptions.End,
            Margin = new(0, 5, 0, 0)
        };
        entryContainer.AddWithSpan(endTimeLabel, 2, 0);

        Label endTimeValue = new()
        {
            Text = breakEndTime,
            Style = LabelStyle,
            Margin = new(5, 5, 0, 0)
        };
        entryContainer.AddWithSpan(endTimeValue, 2, 1, 1, 2);

        // break description

        Border descriptionBorder = new()
        {
            Style = DescriptionBorderStyle,
            WidthRequest = 415,
            Margin = new(0, 0, 0, 5),
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = new(4)
            }
        };
        entryContainer.AddWithSpan(descriptionBorder, 3, 0, 1, 3);

        Label descriptionValue = new()
        {
            Text = description,
            Style = DescriptionLabelStyle,
            VerticalTextAlignment = TextAlignment.Center,
            WidthRequest = 410
        };
        descriptionBorder.Content = descriptionValue;

        // add container to breakDisplay
        breakDisplay.Children.Add(entryContainer);
    }

    // adds an entry box to breakDisplay
    private void AddBreakEntryBox()
    {
        // break entry container

        Grid entryContainer = new()
        {
            RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition()
            },
            ColumnDefinitions =
            {
                new ColumnDefinition(new GridLength(80)),
                new ColumnDefinition(new GridLength(100)),
                new ColumnDefinition(new GridLength(280))
            }
        };

        // top border

        BoxView topBorder = new()
        {
            Style = HorizontalSeparatorStyle
        };
        entryContainer.AddWithSpan(topBorder, 0, 0, 1, 3);

        // start time

        Label startTimeLabel = new()
        {
            Text = "Start Time:",
            Style = LabelStyle,
            HorizontalOptions = LayoutOptions.End,
            Margin = new(0, 5, 0, 0)
        };
        entryContainer.AddWithSpan(startTimeLabel, 1, 0);

        Label startTimeValue = new()
        {
            Style = LabelStyle,
            Margin = new(5, 5, 0, 0)
        };
        startTimeValue.SetBinding(Label.TextProperty, "CurrentBreakStartTime");
        entryContainer.AddWithSpan(startTimeValue, 1, 1, 1, 2);

        // end time

        Label endTimeLabel = new()
        {
            Text = "End Time:",
            Style = LabelStyle,
            HorizontalOptions = LayoutOptions.End,
            Margin = new(0, 5, 0, 0)
        };
        entryContainer.AddWithSpan(endTimeLabel, 2, 0);

        Label endTimeValue = new()
        {
            Text = CreateTimeCardViewModel.DEFAULT_DATETIME,
            Style = LabelStyle,
            Margin = new(5, 5, 0, 0)
        };
        entryContainer.AddWithSpan(endTimeValue, 2, 1, 1, 2);

        // break description

        currentBreakDescriptionBorder = new()
        {
            Style = DescriptionBorderStyle,
            WidthRequest = 415,
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = new(4)
            }
        };
        entryContainer.AddWithSpan(currentBreakDescriptionBorder, 3, 0, 1, 3);

        currentBreakDescriptionValue = new()
        {
            Style = DescriptionLabelStyle,
            VerticalTextAlignment = TextAlignment.Center,
            WidthRequest = 410
        };
        currentBreakDescriptionValue.SetBinding(Label.TextProperty, "CurrentBreakDescription");
        currentBreakDescriptionBorder.Content = currentBreakDescriptionValue;

        currentBreakDescriptionEntryBox = new()
        {
            Placeholder = CreateTimeCardViewModel.BREAK_ENTRY_PLACEHOLDER,
            Style = DescriptionLabelStyle,
            VerticalTextAlignment = TextAlignment.Center,
            WidthRequest = 410
        };
        currentBreakDescriptionEntryBox.SetBinding(IsEnabledProperty, "CurrentBreakDescEntryBoxEnabled");
        currentBreakDescriptionEntryBox.SetBinding(Entry.ReturnCommandProperty, "EnterBreakDescriptionCommand");
        currentBreakDescriptionEntryBox.SetBinding(Entry.ReturnCommandParameterProperty, new Binding
        {
            Source = currentBreakDescriptionEntryBox,
            Path = nameof(Entry.Text)
        });

        // edit description button

        Button editDescriptionButton = new()
        {
            Text = "Edit Break Description",
            Style = ButtonStyle,
            WidthRequest = 275,
            HorizontalOptions = LayoutOptions.Start,
            Margin = new(0,5,0,5)
        };
        editDescriptionButton.SetBinding(Button.CommandProperty, "EditBreakDescriptionCommand");
        entryContainer.AddWithSpan(editDescriptionButton, 4, 0, 1, 3);

        // add container to breakDisplay
        breakDisplay.Children.Add(entryContainer);
    }

    private void SwitchBreakDescriptionLabelWithEntry()
    {
        if (currentBreakDescriptionBorder != null)
        {
            currentBreakDescriptionBorder.Content = currentBreakDescriptionEntryBox;
        }
        else
        {
            App.logger.LogEntry
            (
                $"In {nameof(SwitchBreakDescriptionLabelWithEntry)}:\n",
                $"{nameof(currentBreakDescriptionBorder)} is null"
            );
        }
    }

    private void SwitchBreakDescriptionEntryWithLabel()
    {
        if (currentBreakDescriptionBorder != null)
        {
            currentBreakDescriptionBorder.Content = currentBreakDescriptionValue;
        }
        else
        {
            App.logger.LogEntry
            (
                $"In {nameof(SwitchBreakDescriptionEntryWithLabel)}:\n",
                $"{nameof(currentBreakDescriptionBorder)} is null"
            );
        }
    }

    private void ClearBreakDisplay()
    {
        breakDisplay.Children.Clear();
    }
}