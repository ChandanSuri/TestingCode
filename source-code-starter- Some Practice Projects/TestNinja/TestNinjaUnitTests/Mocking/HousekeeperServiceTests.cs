using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinjaUnitTests.Mocking
{
    [TestFixture]
    class HousekeeperServiceTests
    {
        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private Mock<IUnitOfWork> _unitOfWork;
        private Housekeeper _housekeeper;
        private DateTime _statementDate = new DateTime(2017, 1, 1);
        private readonly string _statementFileName = "filename";

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(_unitOfWork.Object, _statementGenerator.Object,
                                                _emailSender.Object, _messageBox.Object);
        }

        [Test]
        public void  SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_HouseKeepersEmailIsNotGiven_ShouldNotGenerateStatement(string email)
        {
            _housekeeper.Email = email;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(
                            sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
                            Times.Never); // so that it should run Never...
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _statementGenerator.Setup(
                            sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)
                            ).Returns(_statementFileName);

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(_housekeeper.Email, 
                                            _housekeeper.StatementEmailBody,
                                            _statementFileName,
                                            It.IsAny<string>()
                                            ));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileNameNotPresent_ShouldNotEmailTheStatement(string fileName)
        {
            _statementGenerator.Setup(
                            sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)
                            ).Returns(() => fileName); 
            // lambda expression used above because function returns 2 types (overloads) a function as well as string cannot simpley give anything...

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(_housekeeper.Email,
                                            _housekeeper.StatementEmailBody,
                                            _statementFileName,
                                            It.IsAny<string>()
                                            ), Times.Never);
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                )).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }
}
