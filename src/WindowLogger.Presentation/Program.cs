using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WindowLogger.Application.Ports;
using WindowLogger.Application.Workflows;
using WindowLogger.Infrastructure.Adapters;
using WindowLogger.Infrastructure.Exporters;
using WindowLogger.Infrastructure.Repositories;
using WindowLogger.Presentation.Services;
using WindowLogger.Presentation.TrayIcon;
using WindowLogger.Presentation.Forms;

namespace WindowLogger.Presentation;

internal static class Program
{
    [STAThread]
    private static async Task Main(string[] args)
    {
        // Windows Formsアプリケーションの初期化
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

        // ホストビルダーの設定
        var builder = Host.CreateDefaultBuilder(args);
        
        builder.ConfigureServices((context, services) =>
        {
            // ログ設定
            services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.AddDebug();
                configure.SetMinimumLevel(LogLevel.Information);
            });

            // Applicationレイヤー
            services.AddSingleton<WindowActivityWorkflow>();

            // Infrastructureレイヤー
            services.AddSingleton<IWindowActivityRecorder, WindowsApiWindowActivityRecorder>();
            services.AddSingleton<IWindowActivityRepository, FileWindowActivityRepository>();
            services.AddSingleton<IHtmlExporter, FileHtmlExporter>();
            services.AddSingleton<FileHtmlExporter>();

            // Presentationレイヤー
            services.AddSingleton<MainForm>();
            services.AddSingleton<WindowLoggerTrayIcon>();
            services.AddHostedService<WindowActivityBackgroundService>();
        });

        builder.UseConsoleLifetime();

        var host = builder.Build();

        try
        {
            // タスクトレイアイコンを初期化
            var trayIcon = host.Services.GetRequiredService<WindowLoggerTrayIcon>();

            // バックグラウンドサービスを開始
            await host.StartAsync();

            // Windows Formsメッセージループを開始
            System.Windows.Forms.Application.Run();
        }
        catch (Exception ex)
        {
            var loggerFactory = host.Services.GetService<ILoggerFactory>();
            var logger = loggerFactory?.CreateLogger("Program");
            logger?.LogError(ex, "Application startup failed");
            
            System.Windows.Forms.MessageBox.Show($"アプリケーションの起動に失敗しました:\n{ex.Message}", 
                "WindowLogger エラー", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }
        finally
        {
            await host.StopAsync();
            host.Dispose();
        }
    }
}