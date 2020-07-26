﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace IncomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        // Edit Text
        EditText incomePerHourEditText;
        EditText workHourPerDayEditText;
        EditText taxRateEditText;
        EditText savingRatedEditText;

        TextView workSummaryTextView;
        TextView grossIncomeTextView;
        TextView taxtPayableTextView;
        TextView annualSavingTextView;
        TextView spendableIncomeTextView;

        Button calculateButton;
        RelativeLayout resultLayout;

        bool inputCalculated = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
        }

        void ConnectViews() 
        {
            incomePerHourEditText = FindViewById<EditText>(Resource.Id.incomePerHourEditText);
            workHourPerDayEditText = (EditText)FindViewById(Resource.Id.workHoursEditText);
            taxRateEditText = (EditText)FindViewById(Resource.Id.TaxRateEditText);
            savingRatedEditText = (EditText)FindViewById(Resource.Id.savingRateEditText);

            workSummaryTextView = (TextView)FindViewById(Resource.Id.workSummaryTextView);
            grossIncomeTextView = (TextView)FindViewById(Resource.Id.grossIncomeTextView);
            taxtPayableTextView = (TextView)FindViewById(Resource.Id.taxPayableTextView);
            annualSavingTextView = (TextView)FindViewById(Resource.Id.savingTextView);
            spendableIncomeTextView = (TextView)FindViewById(Resource.Id.spendableIncomeTextView);

            calculateButton = (Button)FindViewById(Resource.Id.calculateButton);

            resultLayout = (RelativeLayout)FindViewById(Resource.Id.resultLayout);

            calculateButton.Click += CalculateButton_Click;

        }

        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            if (inputCalculated)
            {
                inputCalculated = false;
                calculateButton.Text = "Calculate";
                ClearInputs();
                return;
            }


            // Take inputs from user
            double incomePerHour = double.Parse(incomePerHourEditText.Text);
            double workHourPerDay = double.Parse(workHourPerDayEditText.Text);
            double taxRate = double.Parse(taxRateEditText.Text);
            double savingsRate = double.Parse(savingRatedEditText.Text);

            double annualWorkHourSummary = workHourPerDay * 5 * 50; // there are 52 weeks in a year, lets assume the user will take 2 week off
            double annualIncome = incomePerHour * workHourPerDay * 5 * 50;
            double taxPayable = (taxRate / 100) * annualIncome;
            double annualSavings = (savingsRate / 100) * annualIncome;
            double spendableIncome = annualIncome - annualSavings - taxPayable;

            //Display results of the calculation
            grossIncomeTextView.Text = annualIncome.ToString("#,##") + " USD";
            workSummaryTextView.Text = annualWorkHourSummary.ToString("#,##") + " HRS";
            taxtPayableTextView.Text = taxPayable.ToString("#,##") + " USD";
            annualSavingTextView.Text = annualSavings.ToString("#,##") + " USD";
            spendableIncomeTextView.Text = spendableIncome.ToString("#,##") + " USD";

            resultLayout.Visibility = Android.Views.ViewStates.Visible;
            inputCalculated = true;
            calculateButton.Text = "Clear";
        }

        void ClearInputs()
        {
            incomePerHourEditText.Text = "";
            workHourPerDayEditText.Text = "";
            taxRateEditText.Text = "";
            savingRatedEditText.Text = "";

            resultLayout.Visibility = Android.Views.ViewStates.Invisible;
        }


    }
}