using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace The_Quiz
{
    public partial class Quiz : ContentPage
    {
        private class Question
        {
            public string Text { get; set; }
            public List<string> Options { get; set; }
            public int CorrectIndex { get; set; }
            public int SelectedIndex { get; set; } = -1;
            public Label FeedbackLabel { get; set; }
        }

        private readonly List<Question> questions = new();

        public Quiz()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayQuestions();
        }

        private void LoadQuestions()
        {
            questions.Add(new Question { Text = "What does 'static' mean in C#?", Options = new() { "Belongs to an instance", "Can’t be inherited", "Belongs to the class", "None" }, CorrectIndex = 2 });
            questions.Add(new Question { Text = "Which keyword is used to inherit a class?", Options = new() { "this", "base", "extends", ":", }, CorrectIndex = 3 });
            questions.Add(new Question { Text = "What is the base class of all classes in C#?", Options = new() { "Object", "Base", "Root", "Core" }, CorrectIndex = 0 });
            questions.Add(new Question { Text = "Which method is used to release unmanaged resources?", Options = new() { "Finalize()", "Release()", "Dispose()", "Clean()" }, CorrectIndex = 2 });
            questions.Add(new Question { Text = "What is boxing in C#?", Options = new() { "Converting int to float", "Converting value type to object", "Packing variables", "Unwrapping classes" }, CorrectIndex = 1 });
            questions.Add(new Question { Text = "What is an interface?", Options = new() { "Class with static methods", "Contract with method signatures", "Class with abstract members", "None" }, CorrectIndex = 1 });
            questions.Add(new Question { Text = "What does async keyword do?", Options = new() { "Blocks the thread", "Makes code run faster", "Allows awaiting asynchronous calls", "None" }, CorrectIndex = 2 });
            questions.Add(new Question { Text = "What is a delegate?", Options = new() { "Method pointer", "Class", "Enum", "Loop type" }, CorrectIndex = 0 });
            questions.Add(new Question { Text = "Which keyword prevents a class from being inherited?", Options = new() { "sealed", "private", "static", "final" }, CorrectIndex = 0 });
            questions.Add(new Question { Text = "Can structs inherit from another class?", Options = new() { "Yes", "Only static classes", "No", "Only interfaces" }, CorrectIndex = 2 });
            questions.Add(new Question { Text = "Which keyword is used to handle exceptions?", Options = new() { "catch", "handle", "throw", "rescue" }, CorrectIndex = 0 });
            questions.Add(new Question { Text = "What is the default access modifier for class members?", Options = new() { "public", "protected", "internal", "private" }, CorrectIndex = 3 });
            questions.Add(new Question { Text = "What is the extension of a compiled C# library?", Options = new() { ".exe", ".cs", ".dll", ".lib" }, CorrectIndex = 2 });
            questions.Add(new Question { Text = "Which of these is a value type?", Options = new() { "class", "object", "interface", "int" }, CorrectIndex = 3 });
            questions.Add(new Question { Text = "Which LINQ method returns the first match?", Options = new() { "Select", "First", "Where", "Find" }, CorrectIndex = 1 });
            questions.Add(new Question { Text = "Which access modifier allows visibility within the same assembly?", Options = new() { "public", "internal", "protected", "private" }, CorrectIndex = 1 });
            questions.Add(new Question { Text = "Which method is the entry point in a C# console app?", Options = new() { "Run()", "Start()", "Main()", "Init()" }, CorrectIndex = 2 });
            questions.Add(new Question { Text = "What does 'var' mean?", Options = new() { "Any value type", "Dynamic type", "Compile-time inferred type", "Object" }, CorrectIndex = 2 });
            questions.Add(new Question { Text = "What does the 'params' keyword do?", Options = new() { "Pass multiple values", "Accept any type", "Add default values", "Return a tuple" }, CorrectIndex = 0 });
            questions.Add(new Question { Text = "Which C# version introduced async/await?", Options = new() { "C# 2.0", "C# 3.0", "C# 4.0", "C# 5.0" }, CorrectIndex = 3 });
        }

        private void DisplayQuestions()
        {
            int index = 0;
            foreach (var q in questions)
            {
                var questionLabel = new Label
                {
                    Text = $"{index + 1}. {q.Text}",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Black
                };
                QuizLayout.Children.Add(questionLabel);

                var group = new StackLayout();
                for (int i = 0; i < q.Options.Count; i++)
                {
                    int localIndex = i;
                    var radio = new RadioButton
                    {
                        Content = q.Options[i],
                        Value = i,
                        GroupName = $"Group{index}"
                    };

                    radio.CheckedChanged += (s, e) =>
                    {
                        if (e.Value)
                            q.SelectedIndex = localIndex;
                    };

                    group.Children.Add(radio);
                }

                QuizLayout.Children.Add(group);

                q.FeedbackLabel = new Label
                {
                    IsVisible = false,
                    FontAttributes = FontAttributes.Italic,
                    FontSize = 14
                };
                QuizLayout.Children.Add(q.FeedbackLabel);

                index++;
            }
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            int score = 0;
            foreach (var q in questions)
            {
                bool isCorrect = q.SelectedIndex == q.CorrectIndex;
                if (isCorrect) score++;

                q.FeedbackLabel.Text = isCorrect
                    ? "✅ Correct!"
                    : $"❌ Incorrect! Correct answer: {q.Options[q.CorrectIndex]}";

                q.FeedbackLabel.TextColor = isCorrect ? Colors.Green : Colors.Red;
                q.FeedbackLabel.IsVisible = true;
            }

            ResultLabel.Text = $"You scored {score} out of {questions.Count}.";
            ResultLabel.IsVisible = true;
        }
    }
}
