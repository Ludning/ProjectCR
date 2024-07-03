using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class StringParserHelper
{

    /// <summary>
    /// { } 파싱
    /// </summary>
    public static List<string> BracesParser(string parserableData)
    {
        List<string> resultList = new List<string>();

        // 정규표현식을 사용하여 { } 안의 문자열을 추출
        Regex regex = new Regex(@"\{(.*?)\}");
        MatchCollection matches = regex.Matches(parserableData);

        foreach (Match match in matches)
        {
            string content = match.Groups[1].Value; // { }를 제외한 내용
            resultList.Add(content);
        }

        return resultList;
    }
    /// <summary>
    /// 콤마 파싱
    /// </summary>
    public static List<string> CommaParser(string parserableData)
    {
        List<string> resultList = new List<string>();

        string[] arr = parserableData.Split(',');
        string[] trimmedArr = arr.Select(s => s.Trim()).ToArray();
        
        return trimmedArr.ToList();
    }
    /// <summary>
    /// : 파싱
    /// </summary>
    public static KeyValuePair<string, string> FirstColonParser(string parserableData)
    {
        // 첫 번째 ':'의 위치를 찾음
        int colonIndex = parserableData.IndexOf(':');
        
        // ':'이 없는 경우 예외 처리
        if (colonIndex == -1)
        {
            throw new ArgumentException("':' 부호가 존재하지 않습니다.");
        }

        // ':' 다음에 '('가 있는지 확인
        int parenthesisIndex = parserableData.IndexOf('(', colonIndex + 1);

        if (parenthesisIndex != -1 && parenthesisIndex < colonIndex)
        {
            throw new ArgumentException("':' 부호가 '(' 부호보다 앞에 있지 않습니다.");
        }

        // 키와 값을 분리
        string key = parserableData.Substring(0, colonIndex).Trim();
        string value = parserableData.Substring(colonIndex + 1).Trim();
        
        // KeyValuePair 반환
        return new KeyValuePair<string, string>(key, value);
    }
    /// <summary>
    /// 괄호 파싱
    /// </summary>
    public static string ParenthesesParser(string parserableData)
    {
        // 문자열이 null 또는 비어 있는지 검사
        if (string.IsNullOrEmpty(parserableData))
        {
            return parserableData; // null 또는 빈 문자열인 경우 그대로 반환
        }

        // 좌우 양끝에 있는 괄호 제거
        if (parserableData.StartsWith("(") && parserableData.EndsWith(")"))
        {
            return parserableData.Substring(1, parserableData.Length - 2);
        }

        // 괄호가 없는 경우 그대로 반환
        return parserableData;
    }
    /// <summary>
    /// | 파싱
    /// </summary>
    public static List<string> PipeParser(string parserableData)
    {
        List<string> resultList = new List<string>();

        string[] arr = parserableData.Split('|');
        string[] trimmedArr = arr.Select(s => s.Trim()).ToArray();
        
        return trimmedArr.ToList();
    }
    
    public static void ParserWeaponConditionEffectData(string parserableData)
    {
        
    }
    public static void ParserWeaponRecordData(string parserableData)
    {
        
    }
}