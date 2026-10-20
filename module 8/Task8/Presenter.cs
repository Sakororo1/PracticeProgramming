using System.Windows;
namespace Task8
{
    internal class Presenter
    {
        Model m; //объект для работы с моделью
        MainWindow mw; //ссылка на окно
        static Presenter Instance;
        private char last_letter;
        private Presenter(MainWindow MW)
        {
            mw = MW;
            m = new Model();
            m.OnWrongLetter += M_OnWrongLetter; //событие выбора неправильной буквы
            m.OnRightLetter += M_OnRightLetter; //событие выбора правильной буквы
            m.OnGotWord += M_OnGotWord; //событие получения слова
            m.OnGuessedWord += M_OnGuessedWord; //событие угадывания слова
            m.OnFinishedHangman += M_OnFinishedHangman;
        }

        private void M_OnFinishedHangman(string str)
        {
            MessageBox.Show("Вы проиграли!");
            for (int i = 0; i < mw.buttons.Length; i++) //отключение всех кнопок
            {
                mw.buttons[i].IsEnabled = false;
            }
            mw.label_word.Content = str;
        }
        private void M_OnGuessedWord()
        {
            MessageBox.Show("Слово угадано!");
            for (int i = 0; i < mw.buttons.Length; i++)
            {
                mw.buttons[i].IsEnabled = false;
            }
        }

        private void M_OnRightLetter(string guessingstring)
        {
            mw.label_word.Content = guessingstring; //обновление значения
            for (int i = 0; i < mw.buttons.Length; i++)
            {
                if (mw.buttons[i].Content.ToString() == last_letter.ToString())
                {
                    mw.buttons[i].IsEnabled = false; //отключение кнопки с последней выбранной буквой
                }
            }
        }
        private void M_OnGotWord(string str)
        {
            mw.label_word.Content = str; //определение слова
        }
        private void DrawHangman() //рисование виселицы
        {
            switch (m.GetMistakes()) //определение количества ошибок
            {
                //показ элемента в зависимости от количества ошибок
                case 1:
                    mw.First.Visibility = Visibility.Visible;
                    return;
                case 2:
                    mw.Second.Visibility = Visibility.Visible;
                    return;
                case 3:
                    mw.Third.Visibility = Visibility.Visible;
                    return;
                case 4:
                    mw.Fourth.Visibility = Visibility.Visible;
                    return;
                case 5:
                    mw.Fifth.Visibility = Visibility.Visible;
                    return;
                case 6:
                    mw.Sixth.Visibility = Visibility.Visible;
                    return;
            }
        }
        private void M_OnWrongLetter()
        {
            for (int i = 0; i < mw.buttons.Length; i++)
            {
                if (mw.buttons[i].Content.ToString() == last_letter.ToString())
                {
                    mw.buttons[i].IsEnabled = false; //отключение кнопки с последней выбранной буквой
                    break;
                }
            }
            DrawHangman();
        }
        public static Presenter GetInstance(MainWindow MW)
        {
            if (Instance == null)
            {
                Instance = new Presenter(MW);
            }
            return Instance;
        }
        public void GetWord() //получение слова
        {
            m.GetWord();

            for (int i = 0; i < mw.buttons.Length; i++)
            {
                mw.buttons[i].IsEnabled = true; //включение всех кнопок
            }
        }
        public void CheckLetter(int tag)
        {
            last_letter = (char)mw.buttons[tag].Content; //определение последней буквы
            m.CheckLetter(last_letter); //проверка буквы
        }
        public void ResetAll() //сброс
        {
            m.ResetAll(); //сброс
            mw.First.Visibility = Visibility.Hidden;
            mw.Second.Visibility = Visibility.Hidden;
            mw.Third.Visibility = Visibility.Hidden;
            mw.Fourth.Visibility = Visibility.Hidden;
            mw.Fifth.Visibility = Visibility.Hidden;
            mw.Sixth.Visibility = Visibility.Hidden;
            GetWord(); //получение нового слова
        }
    }
}
