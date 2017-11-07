using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFramework;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicFramework
{
     public class dbLesson
    {
        DAFramework db;
        public dbLesson(string servername)
        {
            db = new DAFramework(servername);
        }
        public bool InsertLesson(ref string error, int levelID, string lessonName)
        {
            return db.ExcuteNoneQuery("spInsertLesson", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@levelId", levelID),
                new SqlParameter("@lessonName", lessonName)                
                );
        }
        public bool EditNameOfLesson(ref string error, string newName, int turnNumber)
        {
            return db.ExcuteNoneQuery("spEditNameOfLesson", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@newName", newName),
                new SqlParameter("@turnNumber", turnNumber)
                );
        }
        public bool DeleteGetReadyQuestionByLessonId(ref string error, int dateProcess)
        {
            return db.ExcuteNoneQuery("spDeleteGetReadyQuestion", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dateProcess)
                );
        }
        public bool InsertGetReadyQuestion(ref string error,int dayProcess,int turnNumber,string quesContent ,string ansA,string ansB,string ansC,string ansD,string rightAns)
        {
            return db.ExcuteNoneQuery("spInsertGetReadyQuestion", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dayProcess),
                new SqlParameter("@turnNumber", turnNumber),
                new SqlParameter("@quesContent",quesContent),
                  new SqlParameter("@ansA", ansA),
                    new SqlParameter("@ansB", ansB),
                      new SqlParameter("@ansC", ansC),
                        new SqlParameter("@ansD", ansD),
                          new SqlParameter("@rightAns", rightAns)
                );
        }
        public bool InsertKeyWordExes(ref string error, int dayProcess, int turnNumber, string quesContent, string ansA, string ansB, string ansC, string ansD, string rightAns)
        {
            return db.ExcuteNoneQuery("spInsertKeyWordExes", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dayProcess),
                new SqlParameter("@turnNumber", turnNumber),
                new SqlParameter("@quesContent", quesContent),
                  new SqlParameter("@ansA", ansA),
                    new SqlParameter("@ansB", ansB),
                      new SqlParameter("@ansC", ansC),
                        new SqlParameter("@ansD", ansD),
                          new SqlParameter("@rightAns", rightAns)
                );
        }
        public bool DeleteKeyWordExesByLessonId(ref string error, int dateProcess)
        {
            return db.ExcuteNoneQuery("spDeleteKeyWordExesByLessonId", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dateProcess)
                );
        }
        public bool InsertKeyWords(ref string error, int dayProcess, int turnNumber,string keyWord,string howToReadWord,string vieWord,string typeOfWord,string exSentence,byte[] pictureOfWord)
        {
            return db.ExcuteNoneQuery("spInsertKeyWords", CommandType.StoredProcedure,
               ref error,
                new SqlParameter("@dayProcess", dayProcess),
               new SqlParameter("@turnNumber", turnNumber),
               new SqlParameter("@keyWord", keyWord),
               new SqlParameter("@howToReadWord", howToReadWord),
                 new SqlParameter("@vieWord", vieWord),
                   new SqlParameter("@typeOfWord", typeOfWord),
                     new SqlParameter("@exSentence", exSentence),
                       new SqlParameter("@pictureOfWord", pictureOfWord)                     
               );
        }
        public bool DeleteKeyWordByLessonId(ref string error, int dateProcess)
        {
            return db.ExcuteNoneQuery("spDeleteKeyWordByLessonId", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dateProcess)
                );
        }
        public bool InsertSentence(ref string error, int dayProcess, int turnNumber, string keySentence, string howToRead, string vieMeanOfSentence)
        {
            return db.ExcuteNoneQuery("spInsertSentence", CommandType.StoredProcedure,
               ref error,
                new SqlParameter("@dayProcess", dayProcess),
               new SqlParameter("@turnNumber", turnNumber),
               new SqlParameter("@keySentence", keySentence),
                 new SqlParameter("@vieMeanOfSentence", vieMeanOfSentence),
                   new SqlParameter("@howToRead", howToRead)

               );
        }
        public bool DeleteSentencesByLessonId(ref string error, int dateProcess)
        {
            return db.ExcuteNoneQuery("soDeleteSentencesByLessonId", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dateProcess)
                );
        }
        public bool InsertSentenceExes(ref string error, int dayProcess, int turnNumber, string quesContent, string ansA, string ansB, string ansC, string ansD, string rightAns)
        {
            return db.ExcuteNoneQuery("spInsertSentenceExes", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dayProcess),
                new SqlParameter("@turnNumber", turnNumber),
                new SqlParameter("@quesContent", quesContent),
                  new SqlParameter("@ansA", ansA),
                    new SqlParameter("@ansB", ansB),
                      new SqlParameter("@ansC", ansC),
                        new SqlParameter("@ansD", ansD),
                          new SqlParameter("@rightAns", rightAns)
                );
        }
        public bool DeleteSentencesEXByLessonId(ref string error, int dateProcess)
        {
            return db.ExcuteNoneQuery("soDeleteSentencesEXByLessonId", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dateProcess)
                );
        }
        public bool InsertListeningExes(ref string error, int dayProcess, int turnNumber, string quesContent, string ansA, string ansB, string ansC, string ansD, string rightAns)
        {
            return db.ExcuteNoneQuery("spInsertListeningExes", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dayProcess),
                new SqlParameter("@turnNumber", turnNumber),
                new SqlParameter("@quesContent", quesContent),
                  new SqlParameter("@ansA", ansA),
                    new SqlParameter("@ansB", ansB),
                      new SqlParameter("@ansC", ansC),
                        new SqlParameter("@ansD", ansD),
                          new SqlParameter("@rightAns", rightAns)
                );
        }
        public bool DeleteListenQuestionByLessonId(ref string error, int dateProcess)
        {
            return db.ExcuteNoneQuery("spDeleteListenQuestionByLessonId", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dateProcess)
                );
        }
        public bool InsertListenAudioFile(ref string error, int dayProcess,byte[] audioFile)
        {
            return db.ExcuteNoneQuery("spInsertListenAudioFile", CommandType.StoredProcedure,
               ref error,
                new SqlParameter("@dayProcess", dayProcess),
                new SqlParameter("@audiokey",audioFile)

               );
        }
        public bool UpdateListenAudioFile(ref string error, int dateProcess,byte[] audioFile)
        {
            return db.ExcuteNoneQuery("spUpdateListenAudioFile", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dateProcess),
                 new SqlParameter("@audioFile",audioFile)
                );
        }
        public bool InsertListeningExPart2(ref string error, int dayProcess,int turnNumber,string before ,string after,string value)
        {
            return db.ExcuteNoneQuery("spInsertListeningExPart2", CommandType.StoredProcedure,
               ref error,
                new SqlParameter("@dateProcess", dayProcess),
                new SqlParameter("@turnNumber", turnNumber),
                new SqlParameter("@before",before),
                new SqlParameter("@after", after), 
                new SqlParameter("@value", value)
               );
        }
        public bool DeleteListenPart2QuestionByLessonId(ref string error, int dateProcess)
        {
            return db.ExcuteNoneQuery("spDeleteListenPart2QuestionByLessonId", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dateProcess)
                );
        }
        public bool UpdateWordGrammarFile(ref string error, int dateProcess, byte[] wordFile)
        {
            return db.ExcuteNoneQuery("spUpdateWordGrammarFile", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dateProcess),
                 new SqlParameter("@wordFile", wordFile)
                );
        }
        public bool DeleteGrammarQuestionByLessonId(ref string error, int dateProcess)
        {
            return db.ExcuteNoneQuery("spDeleteGrammarQuestionByLessonId", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dateProcess)
                );
        }
        public bool spInsertGrammar(ref string error, int dayProcess, byte[] WordFile)
        {
            return db.ExcuteNoneQuery("spInsertGrammar", CommandType.StoredProcedure,
               ref error,
                new SqlParameter("@dateProcess", dayProcess),
                new SqlParameter("@grammarContent", WordFile)

               );
        }
        public bool InsertGrammarExes(ref string error, int dayProcess, int turnNumber, string quesContent, string ansA, string ansB, string ansC, string ansD, string rightAns)
        {
            return db.ExcuteNoneQuery("spInsertGrammarExes", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@dayProcess", dayProcess),
                new SqlParameter("@turnNumber", turnNumber),
                new SqlParameter("@quesContent", quesContent),
                  new SqlParameter("@ansA", ansA),
                    new SqlParameter("@ansB", ansB),
                      new SqlParameter("@ansC", ansC),
                        new SqlParameter("@ansD", ansD),
                          new SqlParameter("@rightAns", rightAns)
                );
        }
        public bool DeleteLessonByTurnNumber(ref string error, int dayProcess)
        {
            return db.ExcuteNoneQuery("spDeleteLessonByTurnNumber", CommandType.StoredProcedure,
                ref error,
                 new SqlParameter("@turnNumber", dayProcess)
           
                );
        }
        public int GetMaxLessonIndexByLevelId(int levelId)
        {
            return db.GetMaxLessonIndexByLevelId(levelId);
        }
        public DataTable GetGettingReadyQuestionByLessonId(int turnNumber)
        {

            return db.ExecuteQuery("spGetGettingReadyQuestionByLessonId", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public DataTable GetGettingReadyQuestionByLessonId_full(int turnNumber)
        {

            return db.ExecuteQuery("spGetGettingReadyQuestionByLessonId_full", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public DataTable GetKeyWordsLessonId(int turnNumber)
        {
            return db.ExecuteQuery("spGetKeyWordsLessonId", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }   
        public DataTable GetKeyWordExListByLessonId(int turnNumber)
        {
            return db.ExecuteQuery("spGetKeyWordExListByLessonId", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public DataTable GetKeyWordExListByLessonId_full(int turnNumber)
        {
            return db.ExecuteQuery("spGetKeyWordExListByLessonId_full", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public DataTable GetSentenceListByLessonId(int turnNumber)
        {
            return db.ExecuteQuery("spGetSentenceListByLessonId", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
       
        public DataTable GetSentenceExListByLessonId(int turnNumber)
        {
            return db.ExecuteQuery("spGetSentenceExListByLessonId", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public DataTable GetSentenceExListByLessonId_full(int turnNumber)
        {
            return db.ExecuteQuery("spGetSentenceExListByLessonId_full", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public DataTable GetListenQuestionByLessonId(int turnNumber)
        {
            return db.ExecuteQuery("spGetListenQuestionByLessonId", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public DataTable GetListenQuestionByLessonId_full(int turnNumber)
        {
            return db.ExecuteQuery("spGetListenQuestionByLessonId_full", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public DataTable GetListenPart2QuestionListById(int turnNumber)
        {
            return db.ExecuteQuery("spGetListenPart2QuestionListById", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public DataTable GetListenPart2QuestionListById_full(int turnNumber)
        {
            return db.ExecuteQuery("spGetListenPart2QuestionListById_full", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public byte[] GetAudioFileById(int dateProcess)
        {
            return db.GetAudioFileById(dateProcess);
        }
        public DataTable GetGrammarQuestionById(int turnNumber)
        {
            return db.ExecuteQuery("spGetGrammarQuestionById", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public DataTable GetGrammarQuestionById_full(int turnNumber)
        {
            return db.ExecuteQuery("spGetGrammarQuestionById_full", CommandType.StoredProcedure,
                new SqlParameter("@turnNumber", turnNumber));
        }
        public byte[] GetGrammarWordFileByID(int dateProcess)
        {
            return db.GetGrammarWordFileByID(dateProcess);
        }

    }
}
