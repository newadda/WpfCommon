using Common.Common.Converters.TransString.Token;
using System;
using System.Collections.Generic;


namespace Common.Common.Converters.TransString
{

    /// <summary>
    /// 기본 문자열을 포맷팅한 문자열로 바꾼다.
    /// </summary>
    public abstract class StringTransformer
    {
        public OriginalString origin;
        public TransString trans;

        public StringTransformer()
        {
            origin = new OriginalString();
            trans = new TransString();
        }

        public void SetOriginString(string originStr)
        {
            if (originStr == null)
            {
                originStr = "";
            }
            RemoveAndAddTransString(0, trans.ToString().Length, originStr, originStr.Length);

        }

        /// <summary>
        /// 원본 문자열의 커서를 지정
        /// </summary>
        /// <param name="cursor">커서위치</param>
        public void SetOriginCursor(int cursor)
        {
            this.origin.SetCursor(cursor);

            int tempCursor = cursor;
            int transCursor = 0;
            foreach (var token in trans.GetTokenList())
            {
                if (tempCursor <= 0)
                {
                    break;
                }


                int delta = 0;
                if (token is StringToken)
                {
                    if (tempCursor > token.ToString().Length)
                    {
                        delta = token.ToString().Length;
                    }
                    else
                    {
                        delta = tempCursor;
                    }

                    tempCursor -= delta;
                }
                else if (token is ChangeToken)
                {
                    ChangeToken temp = token as ChangeToken;
                    if (tempCursor > temp.GetOriginString().Length)
                    {

                        tempCursor -= temp.GetOriginString().Length;
                        delta = temp.ToString().Length;
                    }
                    else
                    {
                        tempCursor -= tempCursor;
                        delta = temp.ToString().Length;
                    }



                }
                else
                {
                    delta = token.ToString().Length;
                }
                transCursor += delta;



            }

            trans.SetCursor(transCursor);
        }


        /// <summary>
        /// 변경된 문자열에 커서위치 지정
        /// </summary>
        /// <param name="cursor">커서위치</param>
        public void SetTransCursor(int cursor)
        {
            this.trans.SetCursor(cursor);

            int tempCursor = cursor;
            int originCursor = 0;
            foreach (var token in trans.GetTokenList())
            {
            

                if (tempCursor <= 0)
                {
                    break;
                }

                if (tempCursor > token.ToString().Length)
                {
                    if (token is StringToken)
                    {
                        originCursor += token.ToString().Length;
                    }
                    else if (token is ChangeToken)
                    {
                        var temp = token as ChangeToken;
                        originCursor += temp.GetOriginString().ToString().Length;

                    }


                    tempCursor -= token.ToString().Length;



                }
                else
                {
                    if (token is StringToken)
                    {
                        originCursor += tempCursor;
                    }
                    else if (token is ChangeToken)
                    {
                        var temp = token as ChangeToken;
                        originCursor += temp.GetOriginString().ToString().Length;

                    }


                    tempCursor -= tempCursor;
                }


            }
            this.origin.SetCursor(originCursor);
        }

        /// <summary>
        /// 문자열 추가
        /// </summary>
        /// <param name="addStr">추가할 문자열</param>
        public void AddStringToTrans(string addStr)
        {
            this.AddStringToTrans(addStr, trans.GetCursor(), addStr.Length);
        }

        /// <summary>
        /// 문자열 추가
        /// </summary>
        /// <param name="addStr">추가할 문자열</param>
        /// <param name="offset">오프셋</param>
        /// <param name="len">길이</param>
        public void AddStringToTrans(string addStr, int offset, int len)
        {
            SetTransCursor(offset);
            int originCursor = origin.GetCursor();

            String newStr = origin.GetString().Insert(originCursor, addStr.Substring(0, len));

            origin.SetString(newStr);
            List<TokenBase> transList = Convert(newStr);

            trans.GetTokenList().Clear();
            trans.GetTokenList().AddRange(transList);

            SetOriginCursor(originCursor + len);

        }

        /// <summary>
        ///  현재 커서에서 길이만큼 삭제
        /// </summary>
        /// <param name="removeLen">삭제할 길이</param>
        public void RemoveStringToTrans(int removeLen)
        {
            RemoveStringToTrans(trans.GetCursor(), removeLen);
        }

        /// <summary>
        /// 문자열 삭제
        /// </summary>
        /// <param name="offset">커서위치</param>
        /// <param name="removeLen">길이</param>
        public void RemoveStringToTrans(int offset, int removeLen)
        {
            SetTransCursor(offset);
            int originCursor = origin.GetCursor();

            int tempOffset = offset;
            int tempRemoveLen = removeLen;
            List<TokenBase> list = new List<TokenBase>(trans.GetTokenList());
            foreach (var token in list)
            {
                int offsetInToken = 0;
                if (tempRemoveLen <= 0)
                {
                    break;
                }

                if (tempOffset >= token.ToString().Length)
                {
                    tempOffset -= token.ToString().Length;
                    continue;
                }
                else
                {
                    offsetInToken = tempOffset;
                    tempOffset -= tempOffset;

                }
                if (tempOffset <= 0)
                {
                    if (tempRemoveLen >= token.ToString().Length)
                    {
                        tempRemoveLen -= token.ToString().Length;
                        trans.GetTokenList().Remove(token);
                    }
                    else
                    {
                        if (token is StringToken)
                        {
                            StringToken temp = token as StringToken;
                            string newTokenStr = token.ToString().Remove(offsetInToken, tempRemoveLen);
                            temp.setString(newTokenStr);
                        }
                        else
                        {
                            trans.GetTokenList().Remove(token);
                        }

                        tempRemoveLen -= tempRemoveLen;

                    }

                }

            }

            string newStr = trans.ToOriginalString();
            origin.SetString(newStr);

            List<TokenBase> transList = Convert(newStr);

            trans.GetTokenList().Clear();
            trans.GetTokenList().AddRange(transList);

            SetOriginCursor(originCursor);

        }

        protected abstract List<TokenBase> Convert(string originStr);

        public void RemoveAndAddTransString(int offset, int removeLen, string addStr, int addLen)
        {
            RemoveStringToTrans(offset, removeLen);

            AddStringToTrans(addStr, trans.GetCursor(), addLen);
        }

    }
}
