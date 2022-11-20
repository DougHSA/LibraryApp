using LibraryApp.Data;
using LibraryApp.Dto.Book;
using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.Interfaces
{
    interface IBookRepository
    {
        public IEnumerable<BookTb> GetAllBooksRepo();
        public IEnumerable<BookTb> GetBooksFromNameRepo(string name);
        public BookTb GetBooksFromIdRepo(int id);
        public Task<BookTbDto> CreateBookRepo(BookTbDto bookDto);
        public Task<BookTb> UpdateBookRepo(BookTb book);
        public Task<BookTb> DeleteBookRepo(int id);
    }
}
