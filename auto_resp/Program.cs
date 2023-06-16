using System.Text.RegularExpressions;



string RequestSimple(string Line )
{
    Console.Write(Line);
    return Console.ReadLine();
}
string Request(string type,string want,string notwanted,bool wantreadline,string textreadline)
{
    Console.Write(type+" "+want+"/"+notwanted+" :");
    while(true)
    {
        string read = Console.ReadLine();
        read = read.Trim().ToLower();
        if(read == want || read == notwanted)
            {
                if (read == "n") 
                {
                    break;
                }
                else 
                {
                   if (wantreadline)
                   {
                    return RequestSimple( textreadline);
                   }
                   else 
                    {


                        return read;
                    }
                    
                }
            }
        Console.Write(type+" "+want+"/"+notwanted+" :");
    }
    return "";
}

List<RespFunction> tbl = new List<RespFunction>
{
    new RespFunction
    {
        firstPattern = @"\S+:SetSize\(\s*\d+,\d+\s*\)",
        findNumberPattern = @"\d+,\d+",
        thirdPattern = @"\S+:SetSize",
        pos = new Dictionary<int, string>{
            {1, "x"},
            {2, "y"}
        },
    },
   new RespFunction
    {
        firstPattern = @"\S+:SetPos\(\s*\d+,\d+\s*\)",
        findNumberPattern = @"\s*\d+,\d+\s*",
        thirdPattern = @"\S+:SetPos",
        pos = new Dictionary<int, string>{
            {1, "x"},
            {2, "y"}
        },
       
    },
    new RespFunction
    {
        firstPattern = @"\S+:SetTall\(\s*\d+\s*\)",
        findNumberPattern = @"\s*\d+\s*",
        thirdPattern = @"\S+:SetTall",
        pos = new Dictionary<int, string>{
            {1, "y"},
        },
       
    },
    new RespFunction
    {
        firstPattern = @"\S+:SetWide\(\s*\d+\s*\)",
        findNumberPattern = @"\s*\d+\s*",
        thirdPattern = @"\S+:SetWide",
        pos = new Dictionary<int, string>{
            {1, "x"},
        },
    },
    new RespFunction
    {
        firstPattern = @"draw.RoundedBox\(\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\S+s*\)",
        findNumberPattern = @"(?<=\()\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\S+\s*(?=\))",
        thirdPattern = @"draw.RoundedBox",
        pos = new Dictionary<int, string>{
            {1, "y"},
            {2, "x"},
            {3, "y"},
            {4, "x"},
            {5, "y"},
            {6, ""},
        },
    },
    new RespFunction
    {
        firstPattern = @"\S+:DockMargin\(\s*\d+\s*,s*\d+\s*,s*\d+\s*,s*\d+\s*\)",
        findNumberPattern = @"\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*",
        thirdPattern = @"\S+:DockMargin",
        pos = new Dictionary<int, string>{
            {1, "x"},
            {2, "y"},
            {3, "x"},
            {4, "y"},
        },
    },
    new RespFunction
    {
        firstPattern = @"surface.DrawRect\(\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*\)",
        findNumberPattern = @"\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*",
        thirdPattern = @"surface.DrawRect",
        pos = new Dictionary<int, string>{
            {1, "x"},
            {2, "y"},
            {3, "x"},
            {4, "y"},
        },
    },
    new RespFunction
    {
        firstPattern = @"surface.DrawTexturedRect\(\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*\)",
        findNumberPattern = @"\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*",
        thirdPattern = @"surface.DrawTexturedRect",
        pos = new Dictionary<int, string>{
            {1, "x"},
            {2, "y"},
            {3, "x"},
            {4, "y"},
        },
    },
};

string FunctionPersoX = "ScrW()";
string FunctionPersoY = "ScrH()";


string resquest = Request("Function for Responsive","y","n",true,"Functions X : ");
if (resquest != "")
{
    FunctionPersoX = resquest;
    FunctionPersoY = RequestSimple("Functions Y :" );
}
string path = RequestSimple("Path? : ");

string file = File.ReadAllText(path);
foreach(var hehe in tbl)
{
    MatchCollection matches = Regex.Matches(file,hehe.firstPattern);
    foreach(Match v in matches) 
    {
        Match SetSi = Regex.Match(v.Value,hehe.thirdPattern);
        string finalstring = SetSi+"(";
        
        Match v2 = Regex.Match(v.Value, hehe.findNumberPattern);
        string[] val = v2.Value.Split(',');
        int i = 1;
        foreach(string valeur in val)
        {
            
            try
            {
                string type = hehe.pos[i];

                if (double.Parse(valeur) == 0)
                    type = hehe.pos[hehe.pos.Count+1];
                if (type == "x")
                {
                  
                    double valueX = double.Parse(valeur) / 1920;
                    if (i == 1 )
                    {
                        finalstring = finalstring+FunctionPersoX+" * "+valueX.ToString().Replace(',','.'); 
                    }
                    else 
                    {
                        finalstring = finalstring+" , "+FunctionPersoX+" * "+valueX.ToString().Replace(',','.'); 
                    }
                
                }
                else if (type == "y")
                {
                
                    double valueY =  double.Parse(valeur)/ 1080;
                    if (i == 1 )
                    {
                        finalstring = finalstring+FunctionPersoY+" * "+valueY.ToString().Replace(',','.'); 
                    }
                    else 
                    {
                        finalstring = finalstring+" , "+FunctionPersoY+" * "+valueY.ToString().Replace(',','.'); 
                    }
                }
                else 
                {
                 
                    if (i == 1 )
                    {
                       finalstring = finalstring + valeur;
                    }
                    else 
                    {
                        finalstring = finalstring+" , " + valeur;
                    }
                }
            }
            catch
            {
                if (i == 1 )
                {
                    finalstring = finalstring + valeur;
                }
                else 
                {
                    finalstring = finalstring+" , " + valeur;
                }
            }

           
             i++;
        }
     
        finalstring = finalstring + ")";
       
       // finalstring  = SetSi+"("+FunctionPersoX+" * "+valueX.ToString().Replace(',','.')+" , "+FunctionPersoY+" * "+valueY.ToString().Replace(',','.')+" )";
        
        // finish
        file = file.Replace(v.Value,finalstring);
    }


}
string currentpath = AppDomain.CurrentDomain.BaseDirectory;

File.WriteAllText(currentpath+"/"+Path.GetFileName(path)+"_responsive.lua",file);
Console.WriteLine("Done u can close this console !");
Console.ReadLine();


class RespFunction
{
    public string firstPattern { get; set; }
    public string findNumberPattern { get; set; }
    public string thirdPattern { get; set; }
    public Dictionary<int, string> pos { get; set; }
   
}

