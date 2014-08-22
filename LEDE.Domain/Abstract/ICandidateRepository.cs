using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;

namespace LEDE.Domain.Abstract
{
    public interface ICandidateRepository
    {
        IEnumerable<TaskVersion> getTaskVersions(int UserID, int TaskID);

        CandidateIndexModel getIndexModel(int UserID, int? TaskID);

        void uploadTask(int taskID, int userID, Document uploadDoc);

        Document findDocument(int documentID);

        void DeleteTask(int documentID);
    }
}
