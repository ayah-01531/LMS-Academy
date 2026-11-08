namespace DalilAcademy.Core.Exceptions;

// resource does not exist → 404
//الحقل مطلوب مش موجود +الايميل غلط400 
//409 

public class NotFoundException(string message) : Exception(message);
