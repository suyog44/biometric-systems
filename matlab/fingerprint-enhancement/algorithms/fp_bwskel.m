% E.S. Deutsch method for fingerprint image thinning
% ref: E.S.Deutsch,"Thinning algorithms on rectangular, hexagonal, and triangular arrays," Communications of the ACM, 1972.  

function [sk] = fp_bwskel(im_bin)
done = 0;
while ~done
    sk = im_bin;
    im_bin = skel(im_bin);
    done = isequal(sk, im_bin);
end
sk = post_skel(im_bin);

function [out] = skel(B)
out = B;
[m,n] = size(B);
for flag = 1:2
    for k = 2:(m - 1)
        for kk = 2:(n - 1)
            if ~B(k,kk)
                continue;
            end

            P = [B(k,kk+1),B(k-1,kk+1),B(k-1,kk),B(k-1,kk-1),B(k,kk-1),B(k+1,kk-1),B(k+1,kk),B(k+1,kk+1),B(k,kk+1)];
            a = sum(abs(diff(P)));
            if a ~= 0 && a~= 2 && a~= 4
                continue;
            end
            if sum(P(1:8)) == 1
                continue;
            end
            if ((P(1) & P(3) & P(5)) == 0) && ((P(1) & P(3) & P(7)) == 0) && (flag == 1)
                if a ~= 4
                    out(k,kk) = 0;
                    continue;
                elseif ((P(1) & P(7)) == 1) && ((P(2) | P(6)) == 1) && ((P(3) | P(4) | P(5) | P(8)) == 0)
                    out(k,kk) = 0;
                    continue;           
                elseif ((P(1) & P(3)) == 1) && ((P(4) | P(8)) == 1) && ((P(2) | P(5) | P(6) | P(7)) == 0)
                    out(k,kk) = 0;
                    continue;
                end
            end
            if ((P(3) & P(5) & P(7)) == 0) && ((P(5) & P(7) & P(9)) == 0) && (flag == 2)
                if a ~= 4
                    out(k,kk) = 0;
                elseif ((P(3) & P(5)) == 1) && ((P(2) | P(6)) == 1) && ((P(1) | P(4) | P(7) | P(8)) == 0)
                    out(k,kk) = 0;
                elseif ((P(5) & P(7)) == 1) && ((P(4) | P(8)) == 1) && ((P(1) | P(2) | P(3) | P(6)) == 0)
                    out(k,kk) = 0;
                end
            end
        end
    end
    B = out;
end

function [out] = post_skel(B)
[m,n] = size(B);
out = B;
for k = 2:(m - 1)
    for kk = 2:(n - 1)
        if B(k,kk) == 0
            continue;
        end
        P = [B(k,kk+1),B(k-1,kk+1),B(k-1,kk),B(k-1,kk-1),B(k,kk-1),B(k+1,kk-1),B(k+1,kk),B(k+1,kk+1)];
        if sum(P) == 2 && ((P(1) & P(3)) == 1 || (P(3) & P(5)) == 1 || (P(5) & P(7)) == 1 || (P(7) & P(1)) == 1)
            out(k,kk) = 0;
        end
        if sum(P(1:2:7)) == 3
            out(k,kk) = 0;
        end
    end
end
