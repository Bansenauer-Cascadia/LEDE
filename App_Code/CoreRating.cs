using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class CoreRating
{
    private string knowledgec;
    private string knowledgep;
    private string knowledger;
    private string coreRatingID; 


	public CoreRating()
	{
		
	}

    public string CoreRatingID
    {
        get
        {
            return coreRatingID;
        }
        set
        {
            coreRatingID = value;
        }
    }

    public string KnowledgeC
    {
        get
        {
            return knowledgec; 
        }
        set
        {
            knowledgec = value;
        }
    }
    public string KnowledgeP
    {
        get
        {
            return knowledgep;
        }
        set
        {
            knowledgep = value;
        }
    }
    public string KnowledgeR
    {
        get
        {
            return knowledger;
        }
        set
        {
            knowledger = value;
        }
    }
}