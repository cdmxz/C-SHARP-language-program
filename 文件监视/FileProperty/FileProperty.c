#include <Windows.h>

__declspec(dllexport) BOOL WINAPI ShowFileProperty(LPCWSTR fileName);

BOOL WINAPI ShowFileProperty(LPCWSTR fileName)
{
    SHELLEXECUTEINFOW info;
    memset(&info, 0, sizeof(SHELLEXECUTEINFOW));
    info.cbSize = sizeof(SHELLEXECUTEINFOW);
    info.fMask = SEE_MASK_INVOKEIDLIST;
    info.lpVerb = L"properties";
    info.lpFile = fileName;
    info.nShow = SW_SHOW;
    return ShellExecuteExW(&info);
}