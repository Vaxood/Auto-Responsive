# Auto-Responsive
with this application u can auto responsive on gmod like : 
```
somevariablename:SetSize(200,200)
```
this will modify it like that : 
```
somevariablename:SetSize(ScrW() *0.1041666666666667,ScrH()*0.0185185185185185)
```
to auto-responsive !
to add function in auto-responsive program (because im too lazy to add more functions ) follow theses steps : 
download and open project , now do this with this exemple we gonna use SetSize Function : 
```
 new RespFunction // dont modify this
    {
        firstPattern = @"\S+:SetSize\(\s*\d+,\d+\s*\)",  // fist pattern its pattern to find ur function 
        findNumberPattern = @"\d+,\d+", // this its pattern to find agument function 
        thirdPattern = @"\S+:SetSize", // and the last pattern its to get function name 
        pos = new Dictionary<int, string>{  // for SetSize the first its x argument because it wide argument and the seconde its for tall agument so we put y , make sure to put all argument
            {1, "x"},
            {2, "y"}
        },
    },
```
another exemple for draw.RoundedBox function
```
   new RespFunction
    {
        firstPattern = @"draw.RoundedBox\(\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\S+s*\)", // find function
        findNumberPattern = @"(?<=\()\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\S+\s*(?=\))",  // find argument function
        thirdPattern = @"draw.RoundedBox",  // find name function
        pos = new Dictionary<int, string>{  // and i put for the fist agument y because its for tall postion and the same for wide etc... , and for color agument we put just "" to specify we dont need to responsive this one 
            {1, "y"},
            {2, "x"},
            {3, "y"},
            {4, "x"},
            {5, "y"},
            {6, ""},
        },
    },

```

so for finaly my list its look like this :
```

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
        firstPattern = @"draw.RoundedBox\(\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\S+s*\)", // find function
        findNumberPattern = @"(?<=\()\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\d+\s*,\s*\S+\s*(?=\))",  // find argument function
        thirdPattern = @"draw.RoundedBox",  // find name function
        pos = new Dictionary<int, string>{  // and i put for the fist agument y because its for tall postion and the same for wide etc... , and for color agument we put just "" to specify we dont need to responsive this one 
            {1, "y"},
            {2, "x"},
            {3, "y"},
            {4, "x"},
            {5, "y"},
            {6, ""},
        },
    },
};
```

this is very useful for lazy's mans like me :)
video : 
https://youtu.be/bZKVcsuXlwU
