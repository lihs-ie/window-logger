Run dotnet test WindowLogger.sln --configuration Release --no-build --verbosity normal --collect:"XPlat Code Coverage"
Build started 7/19/2025 8:10:03 AM.
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
[xUnit.net 00:00:03.25]   Discovering: WindowLogger.Infrastructure.Tests
[xUnit.net 00:00:03.25]   Discovering: WindowLogger.Domain.Tests
[xUnit.net 00:00:03.25]   Discovering: WindowLogger.Application.Tests
[xUnit.net 00:00:03.38]   Discovered:  WindowLogger.Application.Tests
[xUnit.net 00:00:03.38]   Discovered:  WindowLogger.Domain.Tests
[xUnit.net 00:00:03.38]   Discovered:  WindowLogger.Infrastructure.Tests
[xUnit.net 00:00:03.38]   Starting:    WindowLogger.Infrastructure.Tests
[xUnit.net 00:00:03.38]   Starting:    WindowLogger.Application.Tests
[xUnit.net 00:00:03.38]   Starting:    WindowLogger.Domain.Tests
     Warning:
     The component "Fluent Assertions" is governed by the rules defined in the Xceed License Agreement and
     the Xceed Fluent Assertions Community License. You may use Fluent Assertions free of charge for
     non-commercial use only. An active subscription is required to use Fluent Assertions for commercial use.
     Please contact Xceed Sales mailto:sales@xceed.com to acquire a subscription at a very low cost.
     A paid commercial license supports the development and continued increasing support of
     Fluent Assertions users under both commercial and community licenses. Help us
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
     Warning:
     The component "Fluent Assertions" is governed by the rules defined in the Xceed License Agreement and
     the Xceed Fluent Assertions Community License. You may use Fluent Assertions free of charge for
     non-commercial use only. An active subscription is required to use Fluent Assertions for commercial use.
     Please contact Xceed Sales mailto:sales@xceed.com to acquire a subscription at a very low cost.
     A paid commercial license supports the development and continued increasing support of
     Fluent Assertions users under both commercial and community licenses. Help us
     keep Fluent Assertions at the forefront of unit testing.
     For more information, visit https://xceed.com/products/unit-testing/fluent-assertions/
  Passed WindowLogger.Application.Tests.Ports.IWindowActivityRepositoryTests.LoadWindowActivityLog_ShouldReturnWindowActivityLog [41 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_指定期間の記録を取得できる [38 ms]
  Passed WindowLogger.Application.Tests.Ports.IHtmlExporterTests.ExportToHtml_WithEmptyLog_ShouldReturnValidHtml [53 ms]
  Passed WindowLogger.Application.Tests.Ports.IHtmlExporterTests.ExportToHtml_ShouldReturnHtmlContent [1 ms]
  Passed WindowLogger.Application.Tests.Queries.GetWindowActivityLogQueryTests.Execute_WithExistingRecords_ShouldReturnLogWithRecords [42 ms]
  Passed   Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_アプリケーション名での絞り込みができる [2 ms]
WindowLogger.Application.Tests.Queries.GetWindowActivityLogQueryTests.Execute_ShouldReturnWindowActivityLog [< 1 ms]
  Passed   Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_新しいログを作成できる [3 ms]
WindowLogger.Application.Tests.Workflows.WindowActivityWorkflowTests.ClearAllActivities_ShouldRemoveAllRecords [5 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_全ての記録をクリアできる [< 1 ms]
  Passed WindowLogger.Application.Tests.Workflows.WindowActivityWorkflowTests.RecordCurrentActivity_ShouldRecordAndSaveActivity [< 1 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_複数の記録を追加できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_記録は時系列順でソートされる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_読み取り専用のコレクションを返す [< 1 ms]
  Passed WindowLogger.Domain.Tests.Aggregates.WindowActivityLogTests.WindowActivityLog_記録を追加できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_正常な値で作成できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_異なるIDのインスタンスは等価でない [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_アプリケーション名が推測できない場合は不明になる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_複雑なタイトルからアプリケーション名を推測できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_アプリケーション名を推測できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_時系列順で比較できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.Entities.WindowActivityRecordTests.WindowActivityRecord_同じIDのインスタンスは等価である [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_同じ日時のインスタンスは等価である [53 ms]
  Passed WindowLogger.Application.Tests.Workflows.WindowActivityWorkflowTests.ExportToHtml_ShouldGenerateHtmlContent [< 1 ms]
  Passed WindowLogger.Application.Tests.Workflows.WindowActivityWorkflowTests.GetActivitiesByTimeRange_ShouldReturnFilteredRecords [1 ms]
  Passed WindowLogger.Application.Tests.Workflows.WindowActivityWorkflowTests.GetAllActivities_ShouldReturnAllRecords [< 1 ms]
  Passed WindowLogger.Application.Tests.Commands.RecordWindowActivityCommandTests.Execute_ShouldRecordWindowActivity [63 ms]
  Passed WindowLogger.Application.Tests.Commands.RecordWindowActivityCommandTests.Execute_MultipleCallsRecord_ShouldAddMultipleRecords [< 1 ms]
  Passed WindowLogger.Application.Tests.Ports.IWindowActivityRecorderTests.RecordCurrentWindowActivity_ShouldReturnWindowActivityRecord [1 ms]
  Passed WindowLogger.Application.Tests.Queries.GetWindowActivityRecordsByTimeRangeQueryTests.Execute_WithNoRecordsInRange_ShouldReturnEmptyCollection [< 1 ms]
  Passed WindowLogger.Application.Tests.Queries.GetWindowActivityRecordsByTimeRangeQueryTests.Execute_ShouldReturnRecordsWithinTimeRange [< 1 ms]
  Passed WindowLogger.Application.Tests.Ports.IWindowActivityRepositoryTests.SaveWindowActivityLog_ShouldNotThrow [105 ms]
  Passed WindowLogger.Application.Tests.Services.HtmlExportServiceTests.ExportToHtml_WithSingleRecord_ShouldIncludeRecordData [106 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_正常な日時で作成できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_文字列表現を取得できる [50 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_有効なULIDで作成できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_時系列順序を保持する [22 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_新しいIDを生成できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_同じULIDのインスタンスは等価である [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowActivityRecordIdentifierTests.WindowActivityRecordIdentifier_連続生成で異なるIDが作成される [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_未来の日時では作成できない [119 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_現在時刻で作成できる [1 ms]
[xUnit.net 00:00:03.66]   Finished:    WindowLogger.Application.Tests
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_過去すぎる日時では作成できない [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_ISO8601形式の文字列で表現できる [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.RecordedAtTests.RecordedAt_UTCでない日時は自動的にUTCに変換される [1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_空白文字列では作成できない(invalidTitle: "   ") [195 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_空白文字列では作成できない(invalidTitle: "") [10 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_空白文字列では作成できない(invalidTitle: "\t") [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_空白文字列では作成できない(invalidTitle: "\n") [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_同じ値のインスタンスは等価である [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_null値では作成できない [< 1 ms]
[xUnit.net 00:00:03.67]   Finished:    WindowLogger.Domain.Tests
  Passed WindowLogger.Infrastructure.Tests.Adapters.WindowsApiWindowActivityRecorderTests.RecordCurrentWindowActivity_WhenNoWindowIsActive_ShouldReturnDefaultTitle [40 ms]
  Passed WindowLogger.Infrastructure.Tests.Adapters.WindowsApiWindowActivityRecorderTests.RecordCurrentWindowActivity_ShouldReturnValidRecord [17 ms]
  Passed WindowLogger.Infrastructure.Tests.Adapters.EventDrivenWindowActivityRecorderTests.RecordCurrentWindowActivity_ShouldReturnValidRecord [217 ms]
  Passed WindowLogger.Infrastructure.Tests.Adapters.EventDrivenWindowActivityRecorderTests.Constructor_ShouldInitializeEventHook [< 1 ms]
  Passed WindowLogger.Infrastructure.Tests.Adapters.EventDrivenWindowActivityRecorderTests.Dispose_ShouldCleanupEventHook [6 ms]
  Passed WindowLogger.Infrastructure.Tests.Exporters.FileHtmlExporterTests.ExportToHtml_ShouldCreateHtmlFile [225 ms]
  Passed WindowLogger.Infrastructure.Tests.Exporters.FileHtmlExporterTests.ExportToHtmlFile_ShouldSaveHtmlFileToDirectory [5 ms]
  Passed WindowLogger.Infrastructure.Tests.Exporters.FileHtmlExporterTests.ExportToHtmlFile_ShouldGenerateUniqueFileNames [40 ms]
  Passed WindowLogger.Infrastructure.Tests.Repositories.FileWindowActivityRepositoryTests.SaveWindowActivityLog_ShouldOverwriteExistingFile [372 ms]
[xUnit.net 00:00:03.82]   Finished:    WindowLogger.Infrastructure.Tests
  Passed WindowLogger.Application.Tests.Services.HtmlExportServiceTests.ExportToHtml_WithEmptyLog_ShouldReturnValidHtml [< 1 ms]
  Passed WindowLogger.Domain.Tests.ValueObjects.WindowTitleTests.WindowTitle_正常な値で作成できる [< 1 ms]
  Passed WindowLogger.Application.Tests.Services.HtmlExportServiceTests.ExportToHtml_WithMultipleRecords_ShouldDisplayInTimeOrder [22 ms]
  Passed WindowLogger.Application.Tests.Services.HtmlExportServiceTests.ExportToHtml_ShouldEscapeHtmlCharacters [< 1 ms]
Test Run Successful.
Test Run Successful.
Total tests: 35
Total tests: 20
     Passed: 35
     Passed: 20
 Total time: 8.4104 Seconds
 Total time: 8.4105 Seconds
  Passed WindowLogger.Infrastructure.Tests.Repositories.FileWindowActivityRepositoryTests.LoadWindowActivityLog_ShouldRestoreFromJsonFile [2 ms]
  Passed WindowLogger.Infrastructure.Tests.Repositories.FileWindowActivityRepositoryTests.SaveWindowActivityLog_ShouldCreateJsonFile [2 ms]
  Passed WindowLogger.Infrastructure.Tests.Repositories.FileWindowActivityRepositoryTests.LoadWindowActivityLog_WhenNoFileExists_ShouldReturnEmptyLog [< 1 ms]
Test Run Successful.
Total tests: 12
     Passed: 12
 Total time: 8.5190 Seconds
[xUnit.net 00:00:05.47]   Discovering: WindowLogger.Presentation.Tests
[xUnit.net 00:00:05.50]   Discovered:  WindowLogger.Presentation.Tests
[xUnit.net 00:00:05.50]   Starting:    WindowLogger.Presentation.Tests
  Passed WindowLogger.Presentation.Tests.Forms.MainFormTests.Show_ShouldCallRefreshActivityGrid [1 s]
  Passed WindowLogger.Presentation.Tests.Forms.MainFormTests.Constructor_ShouldCreateUIComponents [24 ms]