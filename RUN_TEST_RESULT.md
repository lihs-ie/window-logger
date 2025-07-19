Run dotnet test WindowLogger.sln --configuration Release --no-build --verbosity normal --collect:"XPlat Code Coverage"
  
Build started 7/19/2025 8:32:12 AM.
     1>Project "D:\a\window-logger\window-logger\WindowLogger.sln" on node 1 (VSTest target(s)).
     1>ValidateSolutionConfiguration:
         Building solution configuration "Release|Any CPU".
Test run for D:\a\window-logger\window-logger\tests\WindowLogger.Domain.Tests\bin\Release\net8.0\WindowLogger.Domain.Tests.dll (.NETCoreApp,Version=v8.0)
Test run for D:\a\window-logger\window-logger\tests\WindowLogger.Application.Tests\bin\Release\net8.0\WindowLogger.Application.Tests.dll (.NETCoreApp,Version=v8.0)
Test run for D:\a\window-logger\window-logger\tests\WindowLogger.Infrastructure.Tests\bin\Release\net8.0\WindowLogger.Infrastructure.Tests.dll (.NETCoreApp,Version=v8.0)
Test run for D:\a\window-logger\window-logger\tests\WindowLogger.Presentation.Tests\bin\Release\net8.0-windows\WindowLogger.Presentation.Tests.dll (.NETCoreApp,Version=v8.0)
VSTest version 17.14.1 (x64)VSTest version 17.14.1 (x64)
VSTest version 17.14.1 (x64)
VSTest version 17.14.1 (x64)
Starting test execution, please wait...
Starting test execution, please wait...
Starting test execution, please wait...
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
A total of 1 test files matched the specified pattern.
A total of 1 test files matched the specified pattern.
A total of 1 test files matched the specified pattern.
[xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v2.4.5+1caef2f33e (64-bit .NET 8.0.18)
[xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v2.4.5+1caef2f33e (64-bit .NET 8.0.18)
[xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v2.4.5+1caef2f33e (64-bit .NET 8.0.18)
[xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v2.4.5+1caef2f33e (64-bit .NET 8.0.18)
[xUnit.net 00:00:03.31]   Discovering: WindowLogger.Application.Tests[xUnit.net 00:00:03.31]   Discovering: WindowLogger.Infrastructure.Tests
[xUnit.net 00:00:03.31]   Discovering: WindowLogger.Domain.Tests
[xUnit.net 00:00:03.43]   Discovered:  WindowLogger.Infrastructure.Tests
[xUnit.net 00:00:03.43]   Discovered:  WindowLogger.Application.Tests
[xUnit.net 00:00:03.43]   Discovered:  WindowLogger.Domain.Tests
[xUnit.net 00:00:03.43]   Starting:    WindowLogger.Application.Tests
[xUnit.net 00:00:03.43]   Starting:    WindowLogger.Domain.Tests
[xUnit.net 00:00:03.43]   Starting:    WindowLogger.Infrastructure.Tests
     Warning:
     The component "Fluent Assertions" is governed by the rules defined in the Xceed License Agreement and
     the Xceed Fluent Assertions Community License. You may use Fluent Assertions free of charge for
     Warning:
     non-commercial use only. An active subscription is required to use Fluent Assertions for commercial use.
     The component "Fluent Assertions" is governed by the rules defined in the Xceed License Agreement and
     Please contact Xceed Sales mailto:sales@xceed.com to acquire a subscription at a very low cost.
     the Xceed Fluent Assertions Community License. You may use Fluent Assertions free of charge for
     A paid commercial license supports the development and continued increasing support of
     Fluent Assertions users under both commercial and community licenses. Help us     non-commercial use only. An active subscription is required to use Fluent Assertions for commercial use.
     Please contact Xceed Sales mailto:sales@xceed.com to acquire a subscription at a very low cost.
     A paid commercial license supports the development and continued increasing support of
     keep Fluent Assertions at the forefront of unit testing.
     Fluent Assertions users under both commercial and community licenses. Help us
     For more information, visit https://xceed.com/products/unit-testing/fluent-assertions/
     keep Fluent Assertions at the forefront of unit testing.
     For more information, visit https://xceed.com/products/unit-testing/fluent-assertions/
     Warning:
     The component "Fluent Assertions" is governed by the rules defined in the Xceed License Agreement and
     the Xceed Fluent Assertions Community License. You may use Fluent Assertions free of charge for
     non-commercial use only. An active subscription is required to use Fluent Assertions for commercial use.
     Please contact Xceed Sales mailto:sales@xceed.com to acquire a subscription at a very low cost.
     A paid commercial license supports the development and continued increasing support of
     Fluent Assertions users under both commercial and community licenses. Help us
     keep Fluent Assertions at the forefront of unit testing.
     For more information, visit https://xceed.com/products/unit-testing/fluent-assertions/
  Passed WindowLogger.Application.Tests.Queries.GetWindowActivityRecordsByTimeRangeQueryTests.Execute_WithNoRecordsInRange_ShouldReturnEmptyCollection [26 ms]
  Passed WindowLogger.Application.Tests.Ports.IWindowActivityRepositoryTests.LoadWindowActivityLog_ShouldReturnWindowActivityLog [26 ms]
  Passed WindowLogger.Application.Tests.Ports.IHtmlExporterTests.ExportToHtml_WithEmptyLog_ShouldReturnValidHtml [44 ms]
  Passed WindowLogger.Application.Tests.Ports.IHtmlExporterTests.ExportToHtml_ShouldReturnHtmlContent [1 ms]
  Passed WindowLogger.Application.Tests.Queries.GetWindowActivityRecordsByTimeRangeQueryTests.Execute_ShouldReturnRecordsWithinTimeRange [4 ms]
  Passed WindowLogger.Application.Tests.Queries.GetWindowActivityLogQueryTests.Execute_WithExistingRecords_ShouldReturnLogWithRecords [1 ms]
  Passed WindowLogger.Application.Tests.Queries.GetWindowActivityLogQueryTests.Execute_ShouldReturnWindowActivityLog [< 1 ms]
  Passed WindowLogger.Application.Tests.Workflows.WindowActivityWorkflowTests.ClearAllActivities_ShouldRemoveAllRecords [< 1 ms]
  Passed WindowLogger.Application.Tests.Workflows.WindowActivityWorkflowTests.RecordCurrentActivity_ShouldRecordAndSaveActivity [< 1 ms]
  Passed WindowLogger.Application.Tests.Workflows.WindowActivityWorkflowTests.ExportToHtml_ShouldGenerateHtmlContent [< 1 ms]
  Passed WindowLogger.Application.Tests.Workflows.WindowActivityWorkflowTests.GetActivitiesByTimeRange_ShouldReturnFilteredRecords [< 1 ms]
  Passed WindowLogger.Application.Tests.Workflows.WindowActivityWorkflowTests.GetAllActivities_ShouldReturnAllRecords [< 1 ms]
  Passed WindowLogger.Application.Tests.Commands.RecordWindowActivityCommandTests.Execute_ShouldRecordWindowActivity [< 1 ms]
  Passed WindowLogger.Application.Tests.Commands.RecordWindowActivityCommandTests.Execute_MultipleCallsRecord_ShouldAddMultipleRecords [< 1 ms]
  Passed WindowLogger.Application.Tests.Ports.IWindowActivityRecorderTests.RecordCurrentWindowActivity_ShouldReturnWindowActivityRecord [8 ms]
  Passed WindowLogger.Application.Tests.Services.HtmlExportServiceTests.ExportToHtml_WithSingleRecord_ShouldIncludeRecordData [90 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_文字列表現を取得できる [37 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_正常な値で作成できる [32 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_有効なULIDで作成できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_指定期間の記録を取得できる [71 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_アプリケーション名での絞り込みができる [2 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_異なるIDのインスタンスは等価でない [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_アプリケーション名が推測できない場合は不明になる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_複雑なタイトルからアプリケーション名を推測できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_アプリケーション名を推測できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_時系列順で比較できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_同じIDのインスタンスは等価である [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_同じ日時のインスタンスは等価である [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_正常な日時で作成できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_新しいログを作成できる [8 ms]
[xUnit.net 00:00:03.67]   Finished:    WindowLogger.Application.Tests
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_全ての記録をクリアできる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_複数の記録を追加できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_記録は時系列順でソートされる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_読み取り専用のコレクションを返す [< 1 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_記録を追加できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_時系列順序を保持する [23 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_新しいIDを生成できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_同じULIDのインスタンスは等価である [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_連続生成で異なるIDが作成される [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_未来の日時では作成できない [112 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_現在時刻で作成できる [1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_過去すぎる日時では作成できない [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_ISO8601形式の文字列で表現できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_UTCでない日時は自動的にUTCに変換される [2 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_空白文字列では作成できない(invalidTitle: "   ") [182 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_空白文字列では作成できない(invalidTitle: "") [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_空白文字列では作成できない(invalidTitle: "\t") [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_空白文字列では作成できない(invalidTitle: "\n") [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_同じ値のインスタンスは等価である [< 1 ms]
[xUnit.net 00:00:03.72]   Finished:    WindowLogger.Domain.Tests
  Passed WindowLogger.Infrastructure.Tests.Adapters.WindowsApiWindowActivityRecorderTests.RecordCurrentWindowActivity_WhenNoWindowIsActive_ShouldReturnDefaultTitle [36 ms]
  Passed WindowLogger.Infrastructure.Tests.Adapters.WindowsApiWindowActivityRecorderTests.RecordCurrentWindowActivity_ShouldReturnValidRecord [2 ms]
  Passed WindowLogger.Infrastructure.Tests.Adapters.EventDrivenWindowActivityRecorderTests.RecordCurrentWindowActivity_ShouldReturnValidRecord [212 ms]
  Passed WindowLogger.Infrastructure.Tests.Adapters.EventDrivenWindowActivityRecorderTests.Constructor_ShouldInitializeEventHook [< 1 ms]
  Passed WindowLogger.Infrastructure.Tests.Adapters.EventDrivenWindowActivityRecorderTests.Dispose_ShouldCleanupEventHook [3 ms]
  Passed WindowLogger.Infrastructure.Tests.Exporters.FileHtmlExporterTests.ExportToHtml_ShouldCreateHtmlFile [229 ms]
  Passed WindowLogger.Infrastructure.Tests.Exporters.FileHtmlExporterTests.ExportToHtmlFile_ShouldSaveHtmlFileToDirectory [4 ms]
  Passed WindowLogger.Infrastructure.Tests.Exporters.FileHtmlExporterTests.ExportToHtmlFile_ShouldGenerateUniqueFileNames [21 ms]
  Passed WindowLogger.Infrastructure.Tests.Repositories.FileWindowActivityRepositoryTests.SaveWindowActivityLog_ShouldOverwriteExistingFile [355 ms]
[xUnit.net 00:00:03.87]   Finished:    WindowLogger.Infrastructure.Tests
  Passed WindowLogger.Application.Tests.Services.HtmlExportServiceTests.ExportToHtml_WithEmptyLog_ShouldReturnValidHtml [< 1 ms]
  Passed WindowLogger.Application.Tests.Services.HtmlExportServiceTests.ExportToHtml_WithMultipleRecords_ShouldDisplayInTimeOrder [23 ms]
  Passed WindowLogger.Application.Tests.Services.HtmlExportServiceTests.ExportToHtml_ShouldEscapeHtmlCharacters [28 ms]
  Passed WindowLogger.Application.Tests.Ports.IWindowActivityRepositoryTests.SaveWindowActivityLog_ShouldNotThrow [91 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_null値では作成できない [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_正常な値で作成できる [< 1 ms]
Test Run Successful.
Total tests: 20
     Passed: 20
Test Run Successful.
Total tests: 35
     Passed: 35
 Total time: 8.4371 Seconds
 Total time: 8.4333 Seconds
  Passed WindowLogger.Infrastructure.Tests.Repositories.FileWindowActivityRepositoryTests.LoadWindowActivityLog_ShouldRestoreFromJsonFile [2 ms]
  Passed WindowLogger.Infrastructure.Tests.Repositories.FileWindowActivityRepositoryTests.SaveWindowActivityLog_ShouldCreateJsonFile [2 ms]
  Passed WindowLogger.Infrastructure.Tests.Repositories.FileWindowActivityRepositoryTests.LoadWindowActivityLog_WhenNoFileExists_ShouldReturnEmptyLog [1 ms]
Test Run Successful.
Total tests: 12
     Passed: 12
 Total time: 8.5867 Seconds
[xUnit.net 00:00:05.48]   Discovering: WindowLogger.Presentation.Tests
[xUnit.net 00:00:05.50]   Discovered:  WindowLogger.Presentation.Tests
[xUnit.net 00:00:05.50]   Starting:    WindowLogger.Presentation.Tests
  Passed WindowLogger.Presentation.Tests.Forms.MainFormTests.Show_ShouldCallRefreshActivityGrid [1 s]
  Passed WindowLogger.Presentation.Tests.Forms.MainFormTests.Constructor_ShouldCreateUIComponents [24 ms]
  Passed WindowLogger.Presentation.Tests.Forms.MainFormTests.ClearButton_ShouldClearAllActivitiesWhenClicked [27 ms]
[xUnit.net 00:00:07.25]     WindowLogger.Presentation.Tests.Forms.MainFormTests.RefreshActivityGrid_WithActivities_ShouldDisplayData [FAIL]
[xUnit.net 00:00:07.25]       Expected dataSource not to be <null>.
[xUnit.net 00:00:07.25]       Stack Trace:
[xUnit.net 00:00:07.26]            at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
[xUnit.net 00:00:07.26]            at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
[xUnit.net 00:00:07.26]            at FluentAssertions.Execution.DefaultAssertionStrategy.HandleFailure(String message)
[xUnit.net 00:00:07.26]            at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
[xUnit.net 00:00:07.26]            at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
[xUnit.net 00:00:07.26]            at FluentAssertions.Execution.AssertionScope.FailWith(String message)
[xUnit.net 00:00:07.26]            at FluentAssertions.Primitives.ReferenceTypeAssertions`2.NotBeNull(String because, Object[] becauseArgs)
[xUnit.net 00:00:07.26]         D:\a\window-logger\window-logger\tests\WindowLogger.Presentation.Tests\Forms\MainFormTests.cs(138,0): at WindowLogger.Presentation.Tests.Forms.MainFormTests.RefreshActivityGrid_WithActivities_ShouldDisplayData()
[xUnit.net 00:00:07.26]            at System.RuntimeMethodHandle.InvokeMethod(Object target, Void** arguments, Signature sig, Boolean isConstructor)
[xUnit.net 00:00:07.26]            at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
[xUnit.net 00:00:07.35]     WindowLogger.Presentation.Tests.Forms.MainFormTests.RefreshActivityGrid_WithEmptyLog_ShouldDisplayCorrectly [FAIL]
[xUnit.net 00:00:07.35]       Expected dataSource not to be <null>.
[xUnit.net 00:00:07.35]       Stack Trace:
[xUnit.net 00:00:07.35]            at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
[xUnit.net 00:00:07.35]            at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
[xUnit.net 00:00:07.35]            at FluentAssertions.Execution.DefaultAssertionStrategy.HandleFailure(String message)
[xUnit.net 00:00:07.35]            at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
[xUnit.net 00:00:07.35]            at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
[xUnit.net 00:00:07.35]            at FluentAssertions.Execution.AssertionScope.FailWith(String message)
[xUnit.net 00:00:07.35]            at FluentAssertions.Primitives.ReferenceTypeAssertions`2.NotBeNull(String because, Object[] becauseArgs)
[xUnit.net 00:00:07.35]         D:\a\window-logger\window-logger\tests\WindowLogger.Presentation.Tests\Forms\MainFormTests.cs(102,0): at WindowLogger.Presentation.Tests.Forms.MainFormTests.RefreshActivityGrid_WithEmptyLog_ShouldDisplayCorrectly()
[xUnit.net 00:00:07.35]            at System.RuntimeMethodHandle.InvokeMethod(Object target, Void** arguments, Signature sig, Boolean isConstructor)
[xUnit.net 00:00:07.35]            at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
[xUnit.net 00:00:07.37]   Finished:    WindowLogger.Presentation.Tests
  Failed WindowLogger.Presentation.Tests.Forms.MainFormTests.RefreshActivityGrid_WithActivities_ShouldDisplayData [311 ms]
  Error Message:
   Expected dataSource not to be <null>.
  Stack Trace:
     at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
   at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
   at FluentAssertions.Execution.DefaultAssertionStrategy.HandleFailure(String message)
   at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
   at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
   at FluentAssertions.Execution.AssertionScope.FailWith(String message)
   at FluentAssertions.Primitives.ReferenceTypeAssertions`2.NotBeNull(String because, Object[] becauseArgs)
   at WindowLogger.Presentation.Tests.Forms.MainFormTests.RefreshActivityGrid_WithActivities_ShouldDisplayData() in D:\a\window-logger\window-logger\tests\WindowLogger.Presentation.Tests\Forms\MainFormTests.cs:line 138
   at System.RuntimeMethodHandle.InvokeMethod(Object target, Void** arguments, Signature sig, Boolean isConstructor)
   at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
  Failed WindowLogger.Presentation.Tests.Forms.MainFormTests.RefreshActivityGrid_WithEmptyLog_ShouldDisplayCorrectly [101 ms]
  Error Message:
   Expected dataSource not to be <null>.
  Stack Trace:
     at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
   at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
   at FluentAssertions.Execution.DefaultAssertionStrategy.HandleFailure(String message)
   at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
   at FluentAssertions.Execution.AssertionScope.FailWith(Func`1 failReasonFunc)
   at FluentAssertions.Execution.AssertionScope.FailWith(String message)
   at FluentAssertions.Primitives.ReferenceTypeAssertions`2.NotBeNull(String because, Object[] becauseArgs)
   at WindowLogger.Presentation.Tests.Forms.MainFormTests.RefreshActivityGrid_WithEmptyLog_ShouldDisplayCorrectly() in D:\a\window-logger\window-logger\tests\WindowLogger.Presentation.Tests\Forms\MainFormTests.cs:line 102
   at System.RuntimeMethodHandle.InvokeMethod(Object target, Void** arguments, Signature sig, Boolean isConstructor)
   at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
  Passed WindowLogger.Presentation.Tests.Forms.MainFormTests.Constructor_ShouldInitializeFormCorrectly [11 ms]
Test Run Failed.
Total tests: 6
     Passed: 4
     Failed: 2
 Total time: 12.0548 Seconds
     1>Project "D:\a\window-logger\window-logger\WindowLogger.sln" (1) is building "D:\a\window-logger\window-logger\tests\WindowLogger.Presentation.Tests\WindowLogger.Presentation.Tests.csproj" (9) on node 1 (VSTest target(s)).
     9>_VSTestConsole:
         MSB4181: The "VSTestTask" task returned false but did not log an error.
     9>Done Building Project "D:\a\window-logger\window-logger\tests\WindowLogger.Presentation.Tests\WindowLogger.Presentation.Tests.csproj" (VSTest target(s)) -- FAILED.
     1>Done Building Project "D:\a\window-logger\window-logger\WindowLogger.sln" (VSTest target(s)) -- FAILED.
Build FAILED.
    0 Warning(s)
    0 Error(s)
Time Elapsed 00:00:14.06
Attachments:
  D:\a\window-logger\window-logger\tests\WindowLogger.Presentation.Tests\TestResults\8d6b1f38-122e-4ff7-b135-df0738b7c827\coverage.cobertura.xml
  D:\a\window-logger\window-logger\tests\WindowLogger.Infrastructure.Tests\TestResults\cc31edee-d6e2-42ce-8ca7-bc3e859996b6\coverage.cobertura.xml
  D:\a\window-logger\window-logger\tests\WindowLogger.Application.Tests\TestResults\0ca0359d-0e88-44af-9fd4-44fc1e248935\coverage.cobertura.xml
  D:\a\window-logger\window-logger\tests\WindowLogger.Domain.Tests\TestResults\f12380ea-62ac-491c-a0cd-ba0d55bd6c0b\coverage.cobertura.xml
Error: Process completed with exit code 1.