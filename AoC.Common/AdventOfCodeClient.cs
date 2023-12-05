using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Common;

public class AdventOfCodeClient
{
    HttpClient _httpClient;
    public AdventOfCodeClient(string session)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://adventofcode.com/");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "session=" + session);
    }

    public async Task<string> GetInput(int year, int day)
    {
        //2023/day/5/input
        HttpResponseMessage response = await _httpClient.GetAsync($"{year}/day/{day}/input");
        return await response.Content.ReadAsStringAsync();
    }
}
