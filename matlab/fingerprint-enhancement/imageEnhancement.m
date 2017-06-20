function varargout = imageEnhancement(varargin)
%IMAGEENHANCEMENT M-file for imageEnhancement.fig
%      IMAGEENHANCEMENT, by itself, creates a new IMAGEENHANCEMENT or raises the existing
%      singleton*.
%
%      H = IMAGEENHANCEMENT returns the handle to a new IMAGEENHANCEMENT or the handle to
%      the existing singleton*.
%
%      IMAGEENHANCEMENT('Property','Value',...) creates a new IMAGEENHANCEMENT using the
%      given property value pairs. Unrecognized properties are passed via
%      varargin to imageEnhancement_OpeningFcn.  This calling syntax produces a
%      warning when there is an existing singleton*.
%
%      IMAGEENHANCEMENT('CALLBACK') and IMAGEENHANCEMENT('CALLBACK',hObject,...) call the
%      local function named CALLBACK in IMAGEENHANCEMENT.M with the given input
%      arguments.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help imageEnhancement

% Last Modified by GUIDE v2.5 28-Apr-2015 12:38:09

% Begin initialization code - DO NOT EDIT

gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @imageEnhancement_OpeningFcn, ...
                   'gui_OutputFcn',  @imageEnhancement_OutputFcn, ...
                   'gui_LayoutFcn',  [], ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
   gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end

% End initialization code - DO NOT EDIT

% --- Executes just before imageEnhancement is made visible.
function imageEnhancement_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   unrecognized PropertyName/PropertyValue pairs from the
%            command line (see VARARGIN)

%load default image
try
    handles.currentImage = rgb2gray(imread('images/1_1.bmp'));
catch
    handles.currentImage =imread('images/1_1.bmp');
end
%imcomplement for correct representation
imshow(handles.currentImage, 'InitialMagnification','fit', 'Parent', handles.originalImage_figure);

axes(handles.originalHistogram_figure);
[pixelCounts grayLevels] = imhist(handles.currentImage);
bar(grayLevels, pixelCounts);
set(handles.originalHistogram_figure,'xtick',[],'ytick',[]);

%display image with default setting in histogram panel
%handles.histOption = 'equalize';
%handles.histogramImage = histeq(handles.currentImage);
handles.histOption = 'strech';
handles.histogramImage=imadjust(handles.currentImage,stretchlim(handles.currentImage),[]);
imshow(handles.histogramImage, 'InitialMagnification','fit', 'Parent', handles.histogram_figure);

%calculate histogram view for this image
axes(handles.histogramFigure);
[pixelCounts grayLevels] = imhist(handles.histogramImage);
bar(grayLevels, pixelCounts);
set(handles.histogramFigure,'xtick',[],'ytick',[]);

%display default value for filter image
handles.filterradius = 3;
handles.filtertype = 'average';
h = fspecial(handles.filtertype, [handles.filterradius handles.filterradius]); 
handles.filteredImage = imfilter(handles.histogramImage,h);
imshow(handles.filteredImage, 'InitialMagnification','fit', 'Parent', handles.enhanced_figure);

%display default value for enhance ridge patterns image
blksze = 8; thresh = 0.1; % parameters to influence the resultant mask - blksize: block size; thresh: threshold of standard deviation of each block 
[normim, mask] = ridgesegment(handles.currentImage, blksze, thresh);
[orientim, reliability] = ridgeorient(handles.filteredImage, 1, 3, 3);
axes(handles.ridgeOrientation_figure);
plotridgeorient(orientim, 20, handles.currentImage, 5);

blksze = 32; 
[freq, medfreq] = ridgefreq(handles.filteredImage, mask, orientim, blksze, 5, 5, 15);
handles.ridgeFrequencyImage = freq;

freq_masked = freq.*mask;
newim = ridgefilter(normim, orientim, freq_masked, 0.5, 0.5, 1);
binim = newim > 0;
handles.enhancedFPImage = binim;

imshow(handles.ridgeFrequencyImage, [min(min(freq)) max(max(freq))], 'InitialMagnification','fit', 'Parent', handles.ridgeFrequency_figure);
imshow(handles.enhancedFPImage, 'InitialMagnification','fit', 'Parent', handles.enhancedFP_figure);

handles.skeleton = fp_bwskel(handles.enhancedFPImage);
imshow(handles.skeleton, 'InitialMagnification','fit', 'Parent', handles.skeleton_figure);
%display default image for skeleton
%param={'bothat','bridge','clean','close','diag','dilate','erode','fill','hbreak','majority','open','remove','shrink','skel','spur','thicken','thin','tophat'};
%for i=1:18 % for all parameter
%    skim(i).image = bwmorph(handles.enhancedFPImage,char(param(i)),Inf);
%       
%end

% Choose default command line output for imageEnhancement
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% UIWAIT makes imageEnhancement wait for user response (see UIRESUME)
% uiwait(handles.figure1);


% --- Outputs from this function are returned to the command line.
function varargout = imageEnhancement_OutputFcn(hObject, eventdata, handles)
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;


% --- Executes on slider movement.
function filterRadiusSlider_Callback(hObject, eventdata, handles)
% hObject    handle to filterRadiusSlider (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'Value') returns position of slider
%        get(hObject,'Min') and get(hObject,'Max') to determine range of slider
handles.filterradius = ceil(get(hObject,'Value'))+1;

switch handles.filtertype
    case 'average'
        handles.filtertype = 'average';
        h = fspecial(handles.filtertype, [handles.filterradius handles.filterradius]); 
        handles.filteredImage = imfilter(handles.histogramImage,h);  
    case 'median'
        handles.filtertype = 'median';
        handles.filteredImage = medfilt2(handles.histogramImage, [handles.filterradius handles.filterradius]);
end

imshow(handles.filteredImage, 'InitialMagnification','fit', 'Parent', handles.enhanced_figure);

%display default value for enhance ridge patterns image
blksze = 8; thresh = 0.1; % parameters to influence the resultant mask - blksize: block size; thresh: threshold of standard deviation of each block 
[normim, mask] = ridgesegment(handles.currentImage, blksze, thresh);
[orientim, reliability] = ridgeorient(handles.filteredImage, 1, 3, 3);
axes(handles.ridgeOrientation_figure);
plotridgeorient(orientim, 20, handles.currentImage, 5);

blksze = 32; 
[freq, medfreq] = ridgefreq(handles.filteredImage, mask, orientim, blksze, 5, 5, 15);
handles.ridgeFrequencyImage = freq;

freq_masked = freq.*mask;
newim = ridgefilter(normim, orientim, freq_masked, 0.5, 0.5, 1);
binim = newim > 0;
handles.enhancedFPImage = binim;

imshow(handles.ridgeFrequencyImage, [min(min(freq)) max(max(freq))], 'InitialMagnification','fit', 'Parent', handles.ridgeFrequency_figure);
imshow(handles.enhancedFPImage, 'InitialMagnification','fit', 'Parent', handles.enhancedFP_figure);

handles.skeleton = fp_bwskel(handles.enhancedFPImage);
imshow(handles.skeleton, 'InitialMagnification','fit', 'Parent', handles.skeleton_figure);

% Choose default command line output for imageEnhancement
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% --- Executes during object creation, after setting all properties.
function filterRadiusSlider_CreateFcn(hObject, eventdata, handles)
% hObject    handle to filterRadiusSlider (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: slider controls usually have a light gray background.
if isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor',[.9 .9 .9]);
end


% --- Executes on selection change in filterSelector.
function filterSelector_Callback(hObject, eventdata, handles)
% hObject    handle to filterSelector (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: contents = cellstr(get(hObject,'String')) returns filterSelector contents as cell array
%        contents{get(hObject,'Value')} returns selected item from filterSelector
contents = cellstr(get(hObject,'String'));
switch contents{get(hObject,'Value')}
    case 'Mean'
        handles.filtertype = 'average';
        h = fspecial(handles.filtertype, [handles.filterradius handles.filterradius]); 
        handles.filteredImage = imfilter(handles.histogramImage,h);      
        %add additional options later
    case 'Median'
        handles.filtertype = 'median';
        handles.filteredImage = medfilt2(handles.histogramImage, [handles.filterradius handles.filterradius]);
end

imshow(handles.filteredImage, 'InitialMagnification','fit', 'Parent', handles.filter_figure);

%display default value for enhance ridge patterns image
blksze = 8; thresh = 0.1; % parameters to influence the resultant mask - blksize: block size; thresh: threshold of standard deviation of each block 
[normim, mask] = ridgesegment(handles.filteredImage, blksze, thresh);
[orientim, reliability] = ridgeorient(handles.filteredImage, 1, 3, 3);

axes(handles.ridgeOrientation_figure);
plotridgeorient(orientim, 20, handles.filteredImage, 5);

blksze = 32; 
[freq, medfreq] = ridgefreq(handles.filteredImage, mask, orientim, blksze, 5, 5, 15);
handles.ridgeFrequencyImage = freq;

freq_masked = freq.*mask;
newim = ridgefilter(normim, orientim, freq_masked, 0.5, 0.5, 1);
binim = newim > 0;
handles.enhancedFPImage = binim;

imshow(handles.ridgeFrequencyImage, [min(min(freq)) max(max(freq))], 'InitialMagnification','fit', 'Parent', handles.ridgeFrequency_figure);
imshow(handles.enhancedFPImage, 'InitialMagnification','fit', 'Parent', handles.enhancedFP_figure);

handles.skeleton = fp_bwskel(handles.enhancedFPImage);
imshow(handles.skeleton, 'InitialMagnification','fit', 'Parent', handles.skeleton_figure);

% Choose default command line output for imageEnhancement
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% --- Executes during object creation, after setting all properties.
function filterSelector_CreateFcn(hObject, eventdata, handles)
% hObject    handle to filterSelector (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: popupmenu controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on selection change in histogramSelector.
function histogramSelector_Callback(hObject, eventdata, handles)
% hObject    handle to histogramSelector (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
contents = cellstr(get(hObject,'String'));
switch contents{get(hObject,'Value')}
    case 'Strech'
        handles.histogramImage=imadjust(handles.currentImage,stretchlim(handles.currentImage),[]); 
        %add additional options later
    case 'Normalization'
        handles.histogramImage=histeq(handles.currentImage);
end

imshow(handles.histogramImage, 'InitialMagnification','fit', 'Parent', handles.histogram_figure);

%calculate histogram view for this image
axes(handles.histogramFigure);
[pixelCounts grayLevels] = imhist(handles.histogramImage);
bar(grayLevels, pixelCounts);
set(handles.histogramFigure,'xtick',[],'ytick',[]);

%display default value for filter image
handles.filterradius = 3;
handles.filtertype = 'average';
h = fspecial(handles.filtertype, [handles.filterradius handles.filterradius]); 
handles.filteredImage = imfilter(handles.histogramImage,h);
imshow(handles.filteredImage, 'InitialMagnification','fit', 'Parent', handles.enhanced_figure);

%display default value for enhance ridge patterns image
blksze = 8; thresh = 0.1; % parameters to influence the resultant mask - blksize: block size; thresh: threshold of standard deviation of each block 
[normim, mask] = ridgesegment(handles.currentImage, blksze, thresh);
[orientim, reliability] = ridgeorient(handles.filteredImage, 1, 3, 3);
axes(handles.ridgeOrientation_figure);
plotridgeorient(orientim, 20, handles.currentImage, 5);

blksze = 32; 
[freq, medfreq] = ridgefreq(handles.filteredImage, mask, orientim, blksze, 5, 5, 15);
handles.ridgeFrequencyImage = freq;

freq_masked = freq.*mask;
newim = ridgefilter(normim, orientim, freq_masked, 0.5, 0.5, 1);
binim = newim > 0;
handles.enhancedFPImage = binim;

imshow(handles.ridgeFrequencyImage, [min(min(freq)) max(max(freq))], 'InitialMagnification','fit', 'Parent', handles.ridgeFrequency_figure);
imshow(handles.enhancedFPImage, 'InitialMagnification','fit', 'Parent', handles.enhancedFP_figure);

handles.skeleton = fp_bwskel(handles.enhancedFPImage);
imshow(handles.skeleton, 'InitialMagnification','fit', 'Parent', handles.skeleton_figure);

% Hints: contents = cellstr(get(hObject,'String')) returns histogramSelector contents as cell array
%        contents{get(hObject,'Value')} returns selected item from histogramSelector

% Choose default command line output for imageEnhancement
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);


% --- Executes during object creation, after setting all properties.
function histogramSelector_CreateFcn(hObject, eventdata, handles)
% hObject    handle to histogramSelector (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: popupmenu controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on selection change in preloadedFiles_menu.
function preloadedFiles_menu_Callback(hObject, eventdata, handles)
% hObject    handle to preloadedFiles_menu (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: contents = cellstr(get(hObject,'String')) returns preloadedFiles_menu contents as cell array
%        contents{get(hObject,'Value')} returns selected item from preloadedFiles_menu
contents = cellstr(get(hObject,'String'));
switch contents{get(hObject,'Value')}
    case 'Fingerprint 1'
        imagePath = 'images/1_1.bmp';    
        %add additional options later
    case 'Fingerprint 2'
        imagePath = 'images/1_3.bmp';   
    case 'Fingerprint 3'
        imagePath = 'images/1_8.bmp';   
end

%load default image
try
    handles.currentImage = rgb2gray(imread(imagePath));
catch
    handles.currentImage =imread(imagePath);
end
%imcomplement for correct representation
imshow(handles.currentImage, 'InitialMagnification','fit', 'Parent', handles.originalImage_figure);

axes(handles.originalHistogram_figure);
[pixelCounts grayLevels] = imhist(handles.currentImage);
bar(grayLevels, pixelCounts);
set(handles.originalHistogram_figure,'xtick',[],'ytick',[]);

%display image with default setting in histogram panel
%handles.histOption = 'equalize';
%handles.histogramImage = histeq(handles.currentImage);
handles.histOption = 'strech';
handles.histogramImage=imadjust(handles.currentImage,stretchlim(handles.currentImage),[]);
imshow(handles.histogramImage, 'InitialMagnification','fit', 'Parent', handles.histogram_figure);

%calculate histogram view for this image
axes(handles.histogramFigure);
[pixelCounts grayLevels] = imhist(handles.histogramImage);
bar(grayLevels, pixelCounts);
set(handles.histogramFigure,'xtick',[],'ytick',[]);

%display default value for filter image
handles.filterradius = 3;
handles.filtertype = 'average';
h = fspecial(handles.filtertype, [handles.filterradius handles.filterradius]); 
handles.filteredImage = imfilter(handles.histogramImage,h);
imshow(handles.filteredImage, 'InitialMagnification','fit', 'Parent', handles.enhanced_figure);

%display default value for enhance ridge patterns image
blksze = 8; thresh = 0.1; % parameters to influence the resultant mask - blksize: block size; thresh: threshold of standard deviation of each block 
[normim, mask] = ridgesegment(handles.currentImage, blksze, thresh);
[orientim, reliability] = ridgeorient(handles.filteredImage, 1, 3, 3);
axes(handles.ridgeOrientation_figure);
plotridgeorient(orientim, 20, handles.currentImage, 5);

blksze = 32; 
[freq, medfreq] = ridgefreq(handles.filteredImage, mask, orientim, blksze, 5, 5, 15);
handles.ridgeFrequencyImage = freq;

freq_masked = freq.*mask;
newim = ridgefilter(normim, orientim, freq_masked, 0.5, 0.5, 1);
binim = newim > 0;
handles.enhancedFPImage = binim;

imshow(handles.ridgeFrequencyImage, [min(min(freq)) max(max(freq))], 'InitialMagnification','fit', 'Parent', handles.ridgeFrequency_figure);
imshow(handles.enhancedFPImage, 'InitialMagnification','fit', 'Parent', handles.enhancedFP_figure);

handles.skeleton = fp_bwskel(handles.enhancedFPImage);
imshow(handles.skeleton, 'InitialMagnification','fit', 'Parent', handles.skeleton_figure);

% Choose default command line output for imageEnhancement
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% --- Executes during object creation, after setting all properties.
function preloadedFiles_menu_CreateFcn(hObject, eventdata, handles)
% hObject    handle to preloadedFiles_menu (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: popupmenu controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on button press in slectFileButtong.
function slectFileButtong_Callback(hObject, eventdata, handles)
% hObject    handle to slectFileButtong (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
[FileName,PathName,FilterIndex] = uigetfile({'*.jpg;*.tif;*.png;*.gif','All Image Files'}, 'Pick an image file');
try 
    handles.currentImage = rgb2gray(imread([PathName, FileName]));
catch
    handles.currentImage = imread([PathName, FileName]);
end
%imcomplement for correct representation
imshow(handles.currentImage, 'InitialMagnification','fit', 'Parent', handles.originalImage_figure);

axes(handles.originalHistogram_figure);
[pixelCounts grayLevels] = imhist(handles.currentImage);
bar(grayLevels, pixelCounts);
set(handles.originalHistogram_figure,'xtick',[],'ytick',[]);

%display image with default setting in histogram panel
%handles.histOption = 'equalize';
%handles.histogramImage = histeq(handles.currentImage);
handles.histOption = 'strech';
handles.histogramImage=imadjust(handles.currentImage,stretchlim(handles.currentImage),[]);
imshow(handles.histogramImage, 'InitialMagnification','fit', 'Parent', handles.histogram_figure);

%calculate histogram view for this image
axes(handles.histogramFigure);
[pixelCounts grayLevels] = imhist(handles.histogramImage);
bar(grayLevels, pixelCounts);
set(handles.histogramFigure,'xtick',[],'ytick',[]);

%display default value for filter image
handles.filterradius = 3;
handles.filtertype = 'average';
h = fspecial(handles.filtertype, [handles.filterradius handles.filterradius]); 
handles.filteredImage = imfilter(handles.histogramImage,h);
imshow(handles.filteredImage, 'InitialMagnification','fit', 'Parent', handles.enhanced_figure);

%display default value for enhance ridge patterns image
blksze = 8; thresh = 0.1; % parameters to influence the resultant mask - blksize: block size; thresh: threshold of standard deviation of each block 
[normim, mask] = ridgesegment(handles.currentImage, blksze, thresh);
[orientim, reliability] = ridgeorient(handles.filteredImage, 1, 3, 3);
axes(handles.ridgeOrientation_figure);
plotridgeorient(orientim, 20, handles.currentImage, 5);

blksze = 32; 
[freq, medfreq] = ridgefreq(handles.filteredImage, mask, orientim, blksze, 5, 5, 15);
handles.ridgeFrequencyImage = freq;

freq_masked = freq.*mask;
newim = ridgefilter(normim, orientim, freq_masked, 0.5, 0.5, 1);
binim = newim > 0;
handles.enhancedFPImage = binim;

imshow(handles.ridgeFrequencyImage, [min(min(freq)) max(max(freq))], 'InitialMagnification','fit', 'Parent', handles.ridgeFrequency_figure);
imshow(handles.enhancedFPImage, 'InitialMagnification','fit', 'Parent', handles.enhancedFP_figure);

handles.skeleton = fp_bwskel(handles.enhancedFPImage);
imshow(handles.skeleton, 'InitialMagnification','fit', 'Parent', handles.skeleton_figure);

% Choose default command line output for imageEnhancement
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);
