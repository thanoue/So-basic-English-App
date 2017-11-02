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
        public static string serverName { get; set; }
        public static string userFullname { get; set; }
        public static string userLoginName { get; set; }
        public static string userPassword { get; set; }
        public static byte[] userAVT { get; set; }
        public static int dateProcess { get; set; }
        public static int role { get; set; }
        public static dbLesson dbLesson;
      //  public static ObservableCollection<GettingReadyQuestion> GettingreadyQuestionList = new ObservableCollection<GettingReadyQuestion>();
        public static void GetGettingRedayList(ObservableCollection<GettingReadyQuestion> GettingreadyQuestionList, int turnNumber,string server)
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
                            ChoseA = true,ChoseB = false,ChoseC = false,ChoseD = false,KeyWord = row["quesContent"].ToString(),AnsA = row["ansA"].ToString(),AnsB = row["ansB"].ToString(),AnsC = row["ansC"].ToString(),AnsD = row["ansD"].ToString(),
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
        public static void GetKeyWordList(ObservableCollection<KeyWord> KeyWordList, int turnNumber, string server)
        {
            dbLesson = new dbLesson(server);
            DataTable temp = dbLesson.GetKeyWordsLessonId(turnNumber);
            foreach (DataRow row in temp.Rows)
            {
                KeyWordList.Add(new KeyWord
                {
                    Index = 0,
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
                SentenceList.Add(new Sentence { Index = 0, KeySentence = i["keySentence"].ToString(), VieMeanOfSentence = i["vieMeanOfSentence"].ToString(), HowToRead = i["howToRead"].ToString() });

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
