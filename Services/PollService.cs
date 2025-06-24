
using SurveyBasket.API.Entities;
using SurveyBasket.API.Persistence;
using System.Threading;

namespace SurveyBasket.API.Services;

public class PollService(ApplicationDbContext context) : IPollService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);
    public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken = default) =>
       await _context.Polls.FindAsync(id, cancellationToken);

    public async Task<Poll?> AddAsync(Poll poll, CancellationToken cancellationToken = default)
    {
        await _context.Polls.AddAsync(poll, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return poll;
    }
    public async Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken = default)
    {
        Poll CurrentPoll = await GetAsync(id, cancellationToken);
        if (CurrentPoll is null) { return false; }
        CurrentPoll.Summary = poll.Summary;
        CurrentPoll.Title = poll.Title;
        CurrentPoll.StartsAt = poll.StartsAt;
        CurrentPoll.EndsAt = poll.EndsAt;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<bool> DeleteAsync(int id, CancellationToken canellationToken = default)
    {
        Poll poll = await GetAsync(id, canellationToken);
        if (poll is null) { return false; }
        _context.Remove(poll);
        await _context.SaveChangesAsync(canellationToken);
        return true;
    }
    public async Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
    {
        Poll poll = await GetAsync(id, cancellationToken);
        if (poll is null) { return false; }
        poll.IsPublished = !poll.IsPublished;
        await _context.SaveChangesAsync(cancellationToken);
        return true;

    }
}
