namespace LeetCode;

public class Solution739
{
    public int[] DailyTemperatures(int[] temperatures)
    {
        var answer = new int[temperatures.Length];
        var stack = new Stack<int>();

        for (var i = temperatures.Length - 1; i >= 0; i--)
        {
            while (stack.Count > 0 && temperatures[stack.Peek()] <= temperatures[i])
                stack.Pop();

            if (stack.Count > 0)
                answer[i] = stack.Peek() - i;
            stack.Push(i);
        }

        return answer;
    }
}
