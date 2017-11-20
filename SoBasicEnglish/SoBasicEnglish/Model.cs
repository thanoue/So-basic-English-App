using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BusinessLogicFramework;
using System.Windows.Media;
using System.Collections.ObjectModel;
namespace SoBasicEnglish
{
    public static class Model
    {

        public static byte[] one { get {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.Digital___One.Save(ms, Properties.Resources.Digital___One.RawFormat);
                return  ms.GetBuffer();
            }
        }
        public static byte[] two
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.Digital___Two.Save(ms, Properties.Resources.Digital___Two.RawFormat);
                return ms.GetBuffer();
            }
        }
        public static byte[] three
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.Digital___Three.Save(ms, Properties.Resources.Digital___Three.RawFormat);
                return ms.GetBuffer();
            }
        }
        public static byte[] four
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.Digital___Four.Save(ms, Properties.Resources.Digital___Four.RawFormat);
                return ms.GetBuffer();
            }
        }
        public static byte[] five
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.Digital___Five.Save(ms, Properties.Resources.Digital___Five.RawFormat);
                return ms.GetBuffer();
            }
        }
        public static byte[] six
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.Digital___Six.Save(ms, Properties.Resources.Digital___Six.RawFormat);
                return ms.GetBuffer();
            }
        }
        public static byte[] eight
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.Digital___Eight.Save(ms, Properties.Resources.Digital___Eight.RawFormat);
                return ms.GetBuffer();
            }
        }
        public static byte[] nine
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.Digital___Nine.Save(ms, Properties.Resources.Digital___Nine.RawFormat);
                return ms.GetBuffer();
            }
        }
        public static byte[] zero
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.Digital___Zero.Save(ms, Properties.Resources.Digital___Zero.RawFormat);
                return ms.GetBuffer();
            }
        }
        public static string serverName { get; set; }
        public static string userFullname { get; set; }
        public static string userLoginName { get; set; }
        public static string userPassword { get; set; }
        public static byte[] userAVT { get; set; }
        public static int dateProcess { get; set; }
        public static int role { get; set; }
        public static dbLesson dbLesson = new dbLesson(serverName); public static string err = "";
        public static void GetGettingReadyList(ObservableCollection<GettingReadyQuestion> GettingreadyQuestionList, int turnNumber,string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetGettingReadyQuestionByLessonId(turnNumber);
            foreach(DataRow row in temp.Rows)
            {
                switch (row["rightAns"].ToString())
                {
                    case "A":
                        GettingreadyQuestionList.Add(new GettingReadyQuestion
                        {
                            ChoseA = false,ChoseB = false,ChoseC = false,ChoseD = false,KeyWord = row["quesContent"].ToString(),AnsA = row["ansA"].ToString(),AnsB = row["ansB"].ToString(),AnsC = row["ansC"].ToString(),AnsD = row["ansD"].ToString(),
                            RightAns = 1
                         }); 
                        break;
                    case "B":
                        GettingreadyQuestionList.Add(new GettingReadyQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesContent"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 2
                        });
                        break;
                    case "C":
                        GettingreadyQuestionList.Add(new GettingReadyQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesContent"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 3
                        });
                        break;
                    case "D":
                        GettingreadyQuestionList.Add(new GettingReadyQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesContent"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 4
                        });
                        break;
                    default:
                        break;
                }
               
            }
        }
        public static void GetGettingReadyList_full(ObservableCollection<GettingReadyQuestion> GettingreadyQuestionList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetGettingReadyQuestionByLessonId_full(turnNumber);
            foreach (DataRow row in temp.Rows)
            {

                switch (row["rightAns"].ToString())
                {
                    case "A":
                        GettingreadyQuestionList.Add(new GettingReadyQuestion
                        {
                            ChoseA = true,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesContent"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 1
                        });
                        break;
                    case "B":
                        GettingreadyQuestionList.Add(new GettingReadyQuestion
                        {
                            ChoseA = false,
                            ChoseB = true,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesContent"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 2
                        });
                        break;
                    case "C":
                        GettingreadyQuestionList.Add(new GettingReadyQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = true,
                            ChoseD = false,
                            KeyWord = row["quesContent"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 3
                        });
                        break;
                    case "D":
                        GettingreadyQuestionList.Add(new GettingReadyQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = true,
                            KeyWord = row["quesContent"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 4
                        });
                        break;
                    default:
                        break;
                }

            }
        }
        public static void GetKeyWordExListByLessonId(ObservableCollection<KeyWordEx> KeyWordExList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetKeyWordExListByLessonId(turnNumber);
            foreach (DataRow row in temp.Rows)
            {

                switch (row["rightAns"].ToString())
                {
                    case "A":
                        KeyWordExList.Add(new KeyWordEx
                        {
                            ChoseA = true,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["keyWord"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 1
                        });
                        break;
                    case "B":
                        KeyWordExList.Add(new KeyWordEx
                        {
                            ChoseA = false,
                            ChoseB = true,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["keyWord"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 2
                        });
                        break;
                    case "C":
                        KeyWordExList.Add(new KeyWordEx
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = true,
                            ChoseD = false,
                            KeyWord = row["keyWord"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 3
                        });
                        break;
                    case "D":
                        KeyWordExList.Add(new KeyWordEx
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = true,
                            KeyWord = row["keyWord"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 4
                        });
                        break;
                    default:
                        break;
                }

            }
        }
        public static void GetKeyWordExListByLessonId_full(ObservableCollection<KeyWordEx> KeyWordExList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetKeyWordExListByLessonId_full(turnNumber);
            foreach (DataRow row in temp.Rows)
            {

                switch (row["rightAns"].ToString())
                {
                    case "A":
                        KeyWordExList.Add(new KeyWordEx
                        {
                            ChoseA = true,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["keyWord"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 1
                        });
                        break;
                    case "B":
                        KeyWordExList.Add(new KeyWordEx
                        {
                            ChoseA = false,
                            ChoseB = true,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["keyWord"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 2
                        });
                        break;
                    case "C":
                        KeyWordExList.Add(new KeyWordEx
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = true,
                            ChoseD = false,
                            KeyWord = row["keyWord"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 3
                        });
                        break;
                    case "D":
                        KeyWordExList.Add(new KeyWordEx
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = true,
                            KeyWord = row["keyWord"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 4
                        });
                        break;
                    default:
                        break;
                }

            }
        }
        public static void GetKeyWordList(ObservableCollection<KeyWord> KeyWordList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetKeyWordsLessonId(turnNumber);
            foreach (DataRow row in temp.Rows)
            {
                KeyWordList.Add(new KeyWord
                {
                    Index = Int32.Parse(row["turnNumber"].ToString()) - 1,
                    Word = row["keyWord"].ToString(),
                    VieWord = row["vieWord"].ToString(),
                    HowToReadWord = row["howToReadWord"].ToString(),
                    TypeOfWord = row["typeOfWord"].ToString(),
                    ExSentence = row["exSentence"].ToString(),
                    PictureOfWord = (byte[])row["pictureOfWord"]
                });

            }
        }
        public static void GetSentenceList(ObservableCollection<Sentence> SentenceList,int turnNumber,string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetSentenceListByLessonId(turnNumber);
            foreach(DataRow i in temp.Rows)
            {
                SentenceList.Add(new Sentence { Index = Int32.Parse(i["turnNumber"].ToString())-1, KeySentence = i["keySentence"].ToString(), VieMeanOfSentence = i["vieMeanOfSentence"].ToString(), HowToRead = i["howToRead"].ToString() });

            }
        }       
        public static void GetSentenceExListByLessonId(ObservableCollection<SentenceEx> SentenceExList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetSentenceExListByLessonId(turnNumber);
            foreach (DataRow row in temp.Rows)
            {

                switch (row["rightAns"].ToString())
                {
                    case "A":
                        SentenceExList.Add(new SentenceEx
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["keySentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 1
                        });
                        break;
                    case "B":
                        SentenceExList.Add(new SentenceEx
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["keySentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 2
                        });
                        break;
                    case "C":
                        SentenceExList.Add(new SentenceEx
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["keySentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 3
                        });
                        break;
                    case "D":
                        SentenceExList.Add(new SentenceEx
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["keySentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 4
                        });
                        break;
                    default:
                        break;
                }

            }
        }
        public static void GetSentenceExListByLessonId_full(ObservableCollection<SentenceEx> SentenceExList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetSentenceExListByLessonId_full(turnNumber);
            foreach (DataRow row in temp.Rows)
            {

                switch (row["rightAns"].ToString())
                {
                    case "A":
                        SentenceExList.Add(new SentenceEx
                        {
                            ChoseA = true,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["keySentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 1
                        });
                        break;
                    case "B":
                        SentenceExList.Add(new SentenceEx
                        {
                            ChoseA = false,
                            ChoseB = true,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["keySentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 2
                        });
                        break;
                    case "C":
                        SentenceExList.Add(new SentenceEx
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = true,
                            ChoseD = false,
                            KeyWord = row["keySentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 3
                        });
                        break;
                    case "D":
                        SentenceExList.Add(new SentenceEx
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = true,
                            KeyWord = row["keySentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 4
                        });
                        break;
                    default:
                        break;
                }

            }
        }
        public static void GetListenQuestionByLessonId(ObservableCollection<ListeningQuestion> ListenQuestionList,int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetListenQuestionByLessonId(turnNumber);
            foreach (DataRow row in temp.Rows)
            {

                switch (row["rightAns"].ToString())
                {
                    case "A":
                        ListenQuestionList.Add(new ListeningQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            QuestionContent = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 1
                        });
                        break;
                    case "B":
                        ListenQuestionList.Add(new ListeningQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            QuestionContent = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 2
                        });
                        break;
                    case "C":
                        ListenQuestionList.Add(new ListeningQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            QuestionContent = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 3
                        });
                        break;
                    case "D":
                        ListenQuestionList.Add(new ListeningQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            QuestionContent = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 4
                        });
                        break;
                    default:
                        break;
                }

            }
        }
        public static void GetListenQuestionByLessonId_full(ObservableCollection<ListeningQuestion> ListenQuestionList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetListenQuestionByLessonId_full(turnNumber);
            foreach (DataRow row in temp.Rows)
            {

                switch (row["rightAns"].ToString())
                {
                    case "A":
                        ListenQuestionList.Add(new ListeningQuestion
                        {
                            ChoseA = true,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            QuestionContent = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 1
                        });
                        break;
                    case "B":
                        ListenQuestionList.Add(new ListeningQuestion
                        {
                            ChoseA = false,
                            ChoseB = true,
                            ChoseC = false,
                            ChoseD = false,
                            QuestionContent = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 2
                        });
                        break;
                    case "C":
                        ListenQuestionList.Add(new ListeningQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = true,
                            ChoseD = false,
                            QuestionContent = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 3
                        });
                        break;
                    case "D":
                        ListenQuestionList.Add(new ListeningQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = true,
                            QuestionContent = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 4
                        });
                        break;
                    default:
                        break;
                }

            }
        }
        public static void GetListenPart2QuestionListById(ObservableCollection<ListeningPart2Question> ListeningPart2QuestionList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetListenPart2QuestionListById(turnNumber);
            foreach (DataRow i in temp.Rows)
            {
                ListeningPart2QuestionList.Add(new ListeningPart2Question { Before = i["beforeText"].ToString(), After = i["afterText"].ToString(), Value = i["Value"].ToString() });

            }
        }
        public static void GetListenPart2QuestionListById_full(ObservableCollection<ListeningPart2Question> ListeningPart2QuestionList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetListenPart2QuestionListById_full(turnNumber);
            foreach (DataRow i in temp.Rows)
            {
                ListeningPart2QuestionList.Add(new ListeningPart2Question { Before = i["beforeText"].ToString(), After = i["afterText"].ToString(), Value = i["Value"].ToString() });

            }
        }
        public static void GetGrammarQuestionById(ObservableCollection<GrammarQuestion> GrammarQuestionList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetGrammarQuestionById(turnNumber);
            foreach (DataRow row in temp.Rows)
            {

                switch (row["rightAns"].ToString())
                {
                    case "A":
                        GrammarQuestionList.Add(new GrammarQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 1
                        });
                        break;
                    case "B":
                        GrammarQuestionList.Add(new GrammarQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 2
                        });
                        break;
                    case "C":
                        GrammarQuestionList.Add(new GrammarQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 3
                        });
                        break;
                    case "D":
                        GrammarQuestionList.Add(new GrammarQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 4
                        });
                        break;
                    default:
                        break;
                }

            }
        }
        public static void GetGrammarQuestionById_full(ObservableCollection<GrammarQuestion> GrammarQuestionList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetGrammarQuestionById_full(turnNumber);
            foreach (DataRow row in temp.Rows)
            {

                switch (row["rightAns"].ToString())
                {
                    case "A":
                        GrammarQuestionList.Add(new GrammarQuestion
                        {
                            ChoseA = true,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 1
                        });
                        break;
                    case "B":
                        GrammarQuestionList.Add(new GrammarQuestion
                        {
                            ChoseA = false,
                            ChoseB = true,
                            ChoseC = false,
                            ChoseD = false,
                            KeyWord = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 2
                        });
                        break;
                    case "C":
                        GrammarQuestionList.Add(new GrammarQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = true,
                            ChoseD = false,
                            KeyWord = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 3
                        });
                        break;
                    case "D":
                        GrammarQuestionList.Add(new GrammarQuestion
                        {
                            ChoseA = false,
                            ChoseB = false,
                            ChoseC = false,
                            ChoseD = true,
                            KeyWord = row["quesSentence"].ToString(),
                            AnsA = row["ansA"].ToString(),
                            AnsB = row["ansB"].ToString(),
                            AnsC = row["ansC"].ToString(),
                            AnsD = row["ansD"].ToString(),
                            RightAns = 4
                        });
                        break;
                    default:
                        break;
                }

            }
        }
        public static bool InsertGetReadyQuestion( ObservableCollection<GettingReadyQuestion> GettingReadyQuestionList,int DateProcess)
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < GettingReadyQuestionList.Count; turnNumber++)
            {
                GettingReadyQuestion i = GettingReadyQuestionList[turnNumber];
                string ans = "";
                if (i.ChoseA)
                    ans = "A";
                if (i.ChoseB)
                    ans = "B";
                if (i.ChoseC)
                    ans = "C";
                if (i.ChoseD)
                    ans = "D";
                result = dbLesson.InsertGetReadyQuestion(ref err, DateProcess, turnNumber + 1, i.KeyWord, i.AnsA, i.AnsB, i.AnsC, i.AnsD, ans);
            }
            return result;
        }
        public static bool InsertKeyWordExes(ObservableCollection<KeyWordEx> KeyWordExList, int DateProcess)
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < KeyWordExList.Count; turnNumber++)
            {
                KeyWordEx i = KeyWordExList[turnNumber];
                string ans = "";
                if (i.ChoseA)
                    ans = "A";
                if (i.ChoseB)
                    ans = "B";
                if (i.ChoseC)
                    ans = "C";
                if (i.ChoseD)
                    ans = "D";
                result = dbLesson.InsertKeyWordExes(ref err, DateProcess, turnNumber + 1, i.KeyWord, i.AnsA, i.AnsB, i.AnsC, i.AnsD, ans);
            }
            return result;
        }
        public static bool InsertKeyWord(ObservableCollection<KeyWord> KeyWordsList, int DateProcess)
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < KeyWordsList.Count; turnNumber++)
            {
                KeyWord i = KeyWordsList[turnNumber];

                result = dbLesson.InsertKeyWords(ref err, DateProcess, turnNumber + 1, i.Word, i.HowToReadWord, i.VieWord, i.TypeOfWord, i.ExSentence, i.PictureOfWord);
            }
            return result;
        }
        public static bool InsertSentences(ObservableCollection<Sentence> SentenceList, int DateProcess)
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < SentenceList.Count; turnNumber++)
            {
                Sentence i = SentenceList[turnNumber];

                result = dbLesson.InsertSentence(ref err, DateProcess, turnNumber + 1, i.KeySentence, i.HowToRead, i.VieMeanOfSentence);
            }
            return result;
        }
        public static bool InsertSentenceExes(ObservableCollection<SentenceEx> SentenceExList, int DateProcess)
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < SentenceExList.Count; turnNumber++)
            {
                SentenceEx i = SentenceExList[turnNumber];
                string ans = "";
                if (i.ChoseA)
                    ans = "A";
                if (i.ChoseB)
                    ans = "B";
                if (i.ChoseC)
                    ans = "C";
                if (i.ChoseD)
                    ans = "D";
                result = dbLesson.InsertSentenceExes(ref err, DateProcess, turnNumber + 1, i.KeyWord, i.AnsA, i.AnsB, i.AnsC, i.AnsD, ans);
            }
            return result;
        }
       public static bool InsertListenAudioFile(byte[] AudioListenFile, int DateProcess)
        {
            //bool result = false;
            return dbLesson.InsertListenAudioFile(ref err, DateProcess, AudioListenFile);
        }
        public static bool InsertListeningExes(ObservableCollection<ListeningQuestion> ListeningQuestionList,int DateProcess)
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < ListeningQuestionList.Count; turnNumber++)
            {
                ListeningQuestion i = ListeningQuestionList[turnNumber];
                string ans = "";
                if (i.ChoseA)
                    ans = "A";
                if (i.ChoseB)
                    ans = "B";
                if (i.ChoseC)
                    ans = "C";
                if (i.ChoseD)
                    ans = "D";
                result = dbLesson.InsertListeningExes(ref err, DateProcess, turnNumber + 1, i.QuestionContent, i.AnsA, i.AnsB, i.AnsC, i.AnsD, ans);
            }
            return result;
        }
        public static bool InsertListeningPart2Exes(ObservableCollection<ListeningPart2Question> ListeningPart2QuestionList, int DateProcess)
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < ListeningPart2QuestionList.Count; turnNumber++)
            {
                ListeningPart2Question i = ListeningPart2QuestionList[turnNumber];

                result = dbLesson.InsertListeningExPart2(ref err, DateProcess, turnNumber + 1, i.Before, i.After, i.Value);
            }
            return result;
        }
        public static bool InsertGrammar(int DateProcess,byte[] WordGrammarFile)
        {
            //bool result = false;
            return dbLesson.spInsertGrammar(ref err, DateProcess, WordGrammarFile);
        }
        public static bool InsertGrammarExes(ObservableCollection<GrammarQuestion> GrammarQuestionList,int DateProcess)
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < GrammarQuestionList.Count; turnNumber++)
            {
                GrammarQuestion i = GrammarQuestionList[turnNumber];
                string ans = "";
                if (i.ChoseA)
                    ans = "A";
                if (i.ChoseB)
                    ans = "B";
                if (i.ChoseC)
                    ans = "C";
                if (i.ChoseD)
                    ans = "D";
                result = dbLesson.InsertGrammarExes(ref err, DateProcess, turnNumber + 1, i.KeyWord, i.AnsA, i.AnsB, i.AnsC, i.AnsD, ans);
            }
            return result;
        }
        public static MemoryStream compress(Image img)
        {
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 60L);
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            MemoryStream ms = new MemoryStream();
            img.Save(ms, jpegCodec, encoderParams);
            return ms;
        }
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
    }
}
