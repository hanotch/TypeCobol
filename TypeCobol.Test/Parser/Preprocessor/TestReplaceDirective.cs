﻿namespace TypeCobol.Test.Parser.Preprocessor
{
    static class TestReplaceDirective
    {
        public static void CheckReplaceSingle()
        {
            string testName = "PgmReplaceSingle";
            string result = PreprocessorUtils.ProcessReplaceDirectives(testName);
            PreprocessorUtils.CheckWithReplaceResultFile(result, testName);
        }

        public static void CheckReplacePartial()
        {
            string testName = "PgmReplacePartial";
            string result = PreprocessorUtils.ProcessReplaceDirectives(testName);
            PreprocessorUtils.CheckWithReplaceResultFile(result, testName);
        }

        public static void CheckReplaceSingleToMultiple()
        {
            string testName = "PgmReplaceSingleToMultiple";
            string result = PreprocessorUtils.ProcessReplaceDirectives(testName);
            PreprocessorUtils.CheckWithReplaceResultFile(result, testName);
        }

        public static void CheckReplaceMultiple()
        {
            string testName = "PgmReplaceMultiple";
            string result = PreprocessorUtils.ProcessReplaceDirectives(testName);
            PreprocessorUtils.CheckWithReplaceResultFile(result, testName);
        }

        public static void CheckReplaceNested()
        {
            string testName = "PgmReplaceNested";
            string result = PreprocessorUtils.ProcessReplaceDirectives(testName);
            PreprocessorUtils.CheckWithReplaceResultFile(result, testName);
        }

        public static void CheckReplaceFunction()
        {
            string testName = "PgmReplaceFunction";
            string result = PreprocessorUtils.ProcessReplaceDirectives(testName);
            PreprocessorUtils.CheckWithReplaceResultFile(result, testName);
        }

        public static void CheckEmptyPartialWordReplace()
        {
            string testName = "PgmEmptyPartialWordReplace";
            string result = PreprocessorUtils.ProcessReplaceDirectives(testName);
            PreprocessorUtils.CheckWithReplaceResultFile(result, testName);
        }
    }
}
