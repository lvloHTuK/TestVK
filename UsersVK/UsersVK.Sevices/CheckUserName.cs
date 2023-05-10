using System;

namespace UsersVK.Sevices
{
    public class CheckUserName
    {
        public char[] ForbiddenCharacter =
        {
            '!','@','#','$','%','^','*','(',
            ')','-','_','+','=','\\','|','{',
            '}','[',']','?',',','.','~','`',
            '"','<','>',':',';','\'','/'
        };

        public bool CheckName(string name)
        {
            var nameCharArray = name.ToCharArray();

            for(int i = 0; i < nameCharArray.Length; i++)
            {
                for(int j = 0; j < ForbiddenCharacter.Length; j++)
                {
                    if (nameCharArray[i] == ForbiddenCharacter[j]) return false;
                }
            }

            return true;
        }
    }
}
